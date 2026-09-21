# Reporte de Auditoría Técnica: Integración BluVoid SaaS ↔ JAIME RAG 2.0

## 1. Resumen Ejecutivo

Actualmente, el sistema se encuentra en un **estado de parálisis operativa** debido a una desalineación crítica entre el código del RAG y el esquema de la base de datos.

*   **Qué funciona**: La subida de archivos al storage y el registro inicial en el SaaS. El RAG recibe las peticiones de ingestión correctamente.
*   **Qué está roto**: La persistencia de vectores (embeddings) y la reconciliación de estados. El RAG intenta escribir en tablas que no existen. El proceso de sincronización del SaaS tiene una lógica circular que le impide "ver" nuevos documentos.
*   **Causa raíz**: El código de **JAIME RAG 2.0** espera una tabla `knowledge_base` y un RPC `match_knowledge_base` que no han sido desplegados en el proyecto de Supabase. Además, el RPC de listado de documentos es autoreferencial y bloquea el avance de estados.

---

## 2. Mapa Real de Arquitectura

| Componente | Rol | Fuente de Verdad / Recurso |
| :--- | :--- | :--- |
| **SaaS (BluVoid)** | Registro Local | `public.knowledge_documents` (Tabla de metadatos/estados) |
| **SaaS (BluVoid)** | Almacenamiento | Bucket `knowledge-documents` (Supabase Storage) |
| **RAG (JAIME 2.0)** | Procesamiento | `/ingest` (FastAPI) + Ollama (qwen3:8b, nomic-embed) |
| **RAG (JAIME 2.0)** | Vector Store | **[ERROR]** Busca `public.knowledge_base` (No existe) |
| **RPCs** | Interfaz | `list_business_documents` (Lógica incorrecta) |

---

## 3. Evidencia Técnica

### A. Desalineación de Esquema
El código en `jaime_rag/memory_node.py` (línea 312) y `document_persistence.py` (línea 59) realiza operaciones sobre la tabla `knowledge_base`.
*   **Estado Real**: La tabla `knowledge_base` **NO EXISTE** en el proyecto `godjricjohnxlsbtqigk`.
*   **Tabla Alternativa**: Existe `public.business_embeddings`, pero está **vacía** y su esquema es incompatible con JAIME 2.0 (usa `source_id` en lugar de `document_id`).

### B. RPCs Faltantes o Erróneos
*   `match_knowledge_base`: **NO EXISTE**. El RAG no puede realizar búsquedas vectoriales (Recall).
*   `list_business_documents`: **EXISTE pero está MAL DISEÑADO**.
    *   *Definición hallada*: Filtra por `kd.status = 'indexed'`.
    *   *Problema*: Dado que el SaaS usa este RPC para mover documentos de `queued` a `indexed`, si el RPC solo devuelve los que ya son `indexed`, un documento nuevo jamás será procesado por el sync.

### C. Fallo en el Ingest (Background Job)
Al recibir `/ingest`, FastAPI responde `200 OK` inmediatamente. El error ocurre en el hilo de fondo al intentar insertar en `knowledge_base`. Como no hay manejo de errores que actualice el status a `error` en la DB registry, el SaaS se queda esperando eternamente en `queued`.

---

## 4. Diagnóstico del documento "Perfumes.txt"

*   **Subido**: SÍ (Existe registro en `knowledge_documents`).
*   **Aceptado por /ingest**: SÍ (Confirmado por logs del RAG).
*   **Procesado / Embeddings**: **NO**. El proceso falló al intentar persistir en la tabla inexistente.
*   **Estado Actual**: `queued` (estancado por fallo en background y lógica de sync circular).

---

## 5. Fases de Trabajo Recomendadas

### Fase 1: Alineación de Infraestructura (Hardening DB)
*   **Objetivo**: Desplegar el esquema real de JAIME 2.0 en Supabase.
*   **Cierre**: Tablas `chat_sessions`, `chat_messages`, `knowledge_base` y `whatsapp_jobs` creadas y con RLS. RPC `match_knowledge_base` funcional.

### Fase 2: Corrección de Interfaz de Sincronización
*   **Objetivo**: Rediseñar `list_business_documents` para que lea de la tabla vectorial (fuente de verdad del RAG) y no de la tabla de documentos (fuente de verdad del SaaS).
*   **Cierre**: El RPC devuelve el conteo de chunks real desde la tabla de vectores.

### Fase 3: Robustez de Ingestión
*   **Objetivo**: Asegurar que el fallo en el background job del RAG se notifique al SaaS.
*   **Cierre**: El RAG actualiza el status del documento a `indexed` (o `error`) al finalizar su tarea.

---

## 6. SQL de Verificación (Hallazgos Reales)

```sql
-- 1. Verificar tablas nucleares (Verás que knowledge_base falta)
SELECT table_name FROM information_schema.tables 
WHERE table_schema = 'public' 
AND table_name IN ('knowledge_base', 'business_embeddings', 'knowledge_documents');

-- 2. Verificar el status de Perfumes.txt
SELECT id, status, document_name FROM public.knowledge_documents 
WHERE document_name = 'Perfumes.txt';

-- 3. Evidencia de la lógica circular del RPC actual
SELECT routine_definition FROM information_schema.routines 
WHERE routine_name = 'list_business_documents';
```
