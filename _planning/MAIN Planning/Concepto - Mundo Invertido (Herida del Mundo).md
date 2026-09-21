# Concepto de Nivel — El Mundo Invertido / La Herida del Mundo

> Origen: boceto a mano (`_planning/BIOMA1.png`) + sesión de diseño con Gemini, 2026-09-20.
> Estado: **concepto, no canon cerrado.** Ver "Preguntas abiertas" antes de construir sobre esto.

## 1. Resumen

El mundo tiene dos capas separadas por **La Herida del Mundo** (ya mencionada como canon en
`Bioma 1 Sotobosque.docx`, ahí en inglés como "Corruption Rifts / Wound of the World"): una capa
"normal" arriba, y una capa **reflejada y más oscura** que aparece a partir del Bioma 3. El HUB
reflejado en Bioma 3 es literalmente el **HUB Claro Central de Bioma 1** — el mismo que ya está
mapeado en `Bioma 1 Sotobosque.docx` (`S0 → S1 → HUB → Ramas A/B/C → S5 → S6 Arena Boss`, con
Sinner IV — La Negación como boss) — pero invertido verticalmente (mirror flip) y tonalmente más
oscuro. No es un HUB nuevo: es el mismo lugar, visto del otro lado de la herida. **Confirmado por
Bernal, 2026-09-20.**

## 2. Lo que muestra el boceto

El dibujo tiene dos mitades separadas por una franja doble roja horizontal etiquetada
**"HERIDA DEL MUNDO"**:

**Mitad superior — HUB normal:**
- Un pasillo de entrada que llega a un **HUB** central (marcado con una curva, como una cúpula o
  claro).
- Desde el HUB salen tres ramas, apiladas: **A** (arriba), **B** (medio), y una tercera flecha sin
  letra visible debajo de B.
- La rama **A** termina en un obstáculo — el texto junto a la flecha dice algo como *"obstacle
  (op)ened by item in [B]"* (parcialmente ilegible en el boceto): un ítem que se consigue en la rama
  B abre el obstáculo de la rama A. Es un item-gate clásico de metroidvania, dentro del mismo HUB.
- Todas las ramas apuntan hacia una elipse a la derecha marcada **"Boos ROOM"** (Boss Room) —
  en el boceto aparece una sola elipse, no está claro si las tres rutas convergen en la misma arena
  o si es solo una notación abreviada de "aquí va el boss, una por rama."
- Hay una caja de texto **"ITEM to open A"** que se dibuja justo encima de la línea de la Herida,
  como si el ítem viviera en el límite entre ambas capas.

**Mitad inferior — HUB reflejado (Bioma 3):**
- El mismo layout — HUB, ramas A/B, Boss Room — pero **volteado verticalmente y con el texto
  invertido** (literalmente hay que girar la imagen para leerlo).
- Está etiquetado explícitamente: **"HUB BIOMA 3, MUNDO reflejado, es mas oscuro."**
- La misma caja "ITEM to open A" se repite, también invertida, en el mismo punto relativo respecto
  a la Herida.

## 3. La idea central

La Herida del Mundo no es solo lore de fondo — es un **eje de simetría estructural** para el diseño
de niveles. Un hub que el jugador ya conoce (de un bioma anterior) vuelve a aparecer más adelante en
el Bioma 3, con la misma topología pero invertida y ensombrecida. El jugador reconoce el espacio
antes de reconocer conscientemente que es un reflejo — la extrañeza viene del "ya estuve aquí, pero
no así."

Esto encaja con el tema ya establecido en el canon (`BIOMA INICIAL...docx`): el mundo no está roto,
está **estancado / repitiéndose** — Sinner IV, La Negación, es literalmente sobre rutinas que se
repiten sin sentido. Un hub que se repite invertido en otro bioma es la misma idea a escala de
mapa.

## 4. Preguntas — estado

- ✅ **¿Bioma 2 o Bioma 3? — Resuelto.** El HUB de arriba en el boceto es el HUB de **Bioma 1**
  (Claro Central, ya canon, termina en su propia Boss Room con La Negación). El reflejo pisable
  aparece en **Bioma 3**. "A partir del bioma 2" describe cuándo el jugador empieza a *sentir* que
  algo se repite/tuerce — el tramo intermedio antes de llegar al reflejo real — no un segundo HUB
  reflejado en Bioma 2.
- ✅ **¿Una Boss Room o dos? — Resuelto.** Son **dos salas distintas**, una por capa: la Boss Room de
  Bioma 1 (La Negación) arriba, y su reflejo — más oscuro, misma geometría, distinto espacio — en
  Bioma 3 abajo. No es una arena compartida que cruza la Herida.
- ⬜ **¿El ítem cruza la Herida?** ¿El ítem que abre la rama A del HUB reflejado se consigue en la
  rama B del HUB de Bioma 1 (cruzando capas), o cada capa es autocontenida y solo comparte la
  *estructura*, no el progreso?
- ⬜ **¿Cuántos HUBs se reflejan?** ¿Es solo el HUB de Bioma 1 el que reaparece invertido en Bioma 3,
  o cada bioma tiene su contraparte reflejada más adelante (un patrón repetible)?

## 5. Dónde vive esto en el proyecto

Este documento vive junto a los otros documentos de bioma en `_planning/MAIN Planning/`, pero **no es
parte de ningún bioma específico** — es un concepto de estructura de mapa que conecta el HUB de
Bioma 1 con Bioma 3. Referenciarlo desde:
- `Bioma 1 Sotobosque.docx` / `BIOMA INICIAL...docx` — al diseñar el HUB de la demo (Fase 04, S-HUB
  del grafo `S0 → S1 → HUB → A/B/C → S5 → S6`), tener en cuenta si ese HUB es el que luego se
  refleja.
- El futuro documento de diseño del Bioma 3, cuando exista — debe construirse *a partir de* este
  concepto, no al revés.
- El boceto original queda en `_planning/BIOMA1.png` — este documento es su transcripción/expansión
  en texto, no lo reemplaza.

No es bloqueante para la Fase 02 (tileset) ni la Fase 03 (movimiento) en curso — es contexto de
diseño para cuando el roadmap llegue a Bioma 3 (más adelante en las 11 fases). Vale la pena que
Planner lo tenga presente al planificar esas fases, pero no requiere acción de Coder ahora.
