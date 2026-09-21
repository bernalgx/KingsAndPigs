# Phase 01: From tutorial to game Redesign + Theme System

## Objetivo

1...

2...

3...

---

## Contexto técnico (auditado)

## Files to Read (Coder)

- `.agents/rules/global.md`

- `.agents/rules/coder.md`

- `_planning/assets/palette.md`

- `_planning/assets/brand.md`

---KingsAndPigs
/ProjectSettings/

KingsAndPigs
/Assets/

## Tarea 1 â€” Auditar `styles/main.css`

Antes de modificar, reportar:

- ¿Existe `[data-theme="dark"]`, `.dark`, o `@media prefers-color-scheme`? â†’ **NO** (verificado)

- ¿Las CSS variables actuales son dark o light? â†’ **Dark** (`--color-bg: #09090b`, `--color-text-primary: #fafafa`)

- Tailwind `darkMode` ya está configurado como `['class', '[data-theme="dark"]']` â†’ âœ… compatible

---

## Tarea 2 â€” Definir dos paletas en `styles/main.css`

Usar `@layer base` dentro de Tailwind v3. Mantener los MISMOS nombres de variables.

```css
@layer base {
  :root {
    /* Light theme (default) */

    --color-primary: #6f5b98;

    --color-primary-dark: #423558;

    --color-accent: #b7861b;

    --color-accent-light: #d4a535;

    --color-bg: #c5ccd5;

    --color-surface: #f9f8fc;

    --color-surface-raised: #ffffff;

    --color-border: rgba(111, 91, 152, 0.12);

    --color-border-subtle: rgba(111, 91, 152, 0.06);

    --color-text-primary: #423558;

    --color-text-muted: #766096;

    --color-text-subtle: rgba(66, 53, 88, 0.5);

    --color-focus-ring: #6f5b98;

    --color-destructive: #ef4444;

    --color-success: #10b981;
  }

  [data-theme="dark"] {
    --color-primary: #9b84cc;

    --color-primary-dark: #1a1228;

    --color-accent: #d4a535;

    --color-accent-light: #e8bf5a;

    --color-bg: #0f0a1a;

    --color-surface: #1c1430;

    --color-surface-raised: #251b3d;

    --color-border: rgba(155, 132, 204, 0.15);

    --color-border-subtle: rgba(155, 132, 204, 0.08);

    --color-text-primary: #e8e0f5;

    --color-text-muted: #a899c7;

    --color-text-subtle: rgba(232, 224, 245, 0.5);

    --color-focus-ring: #9b84cc;

    --color-destructive: #f87171;

    --color-success: #34d399;
  }
}
```

---

## Tarea 3 â€” Instalar y configurar `next-themes`

```bash


npm install next-themes


```

En `app/layout.tsx`:

```tsx
import { ThemeProvider } from "next-themes";

// Dentro del return:

<html lang="es" className={montserrat.variable} suppressHydrationWarning>
  <body className="bg-bg text-text-primary font-sans">
    <ThemeProvider
      attribute="data-theme"
      defaultTheme="system"
      enableSystem={true}
      disableTransitionOnChange={false}
    >
      {/* existing children */}
    </ThemeProvider>
  </body>
</html>;
```

---

## Tarea 4 â€” Instalar lucide-react y crear ThemeToggle

```bash


npm install lucide-react


```

Crear `components/ui/ThemeToggle.tsx`:

```tsx
"use client";

import { useTheme } from "next-themes";

import { Sun, Moon } from "lucide-react";

// Botón circular: bg-surface border-primary/20 rounded-full p-2

// Animación: rotate + scale en transición (CSS transition 0.2s)

// Sin texto â€” solo ícono

// Manejar mounted state para evitar hydration mismatch
```

---

## Tarea 5 â€” Agregar ThemeToggle al TenantSidebar

Ubicar en el footer del sidebar, justo encima del SubscriptionBadge.

---

## Tarea 6 â€” Agregar transición suave entre temas

En `styles/main.css`, agregar regla global:

```css
* {
  transition:
    background-color 0.2s ease,
    color 0.2s ease,
    border-color 0.2s ease;
}
```

---

## Files to Modify

### 1. `styles/main.css`

- Eliminar `@import` de Google Fonts

- Envolver variables en `@layer base` con `:root` (light) y `[data-theme="dark"]`

- Actualizar `--font-sans` para Montserrat

- Agregar transición suave global

### 2. `tailwind.config.js`

- Agregar `brand: { ... }` tokens en `theme.extend.colors`

### 3. `app/layout.tsx`

- Importar Montserrat vía `next/font/google`

- Agregar `ThemeProvider` de `next-themes`

- `suppressHydrationWarning` en `<html>`

### 4. `components/ui/ThemeToggle.tsx` [NEW]

- Toggle de tema dark/light con iconos Sun/Moon

### 5. `components/ui/TenantSidebar/TenantSidebar.tsx`

- Reemplazar zinc-\* â†’ brand tokens

- Agregar ThemeToggle al footer del sidebar

### 6. `app/b/[businessId]/layout.tsx`

- Agregar `bg-bg` al main content

### 7. `components/ui/PageHeader.tsx`

- Actualizar badge a brand tokens

---

## Acceptance Criteria

### Brand & Variables

- [ ] CSS variables en `:root` apuntan a paleta light de marca

- [ ] CSS variables en `[data-theme="dark"]` apuntan a paleta dark

- [ ] `npm run build` pasa â€” 0 errores

- [ ] `npm run lint` pasa â€” 0 warnings

### Theme System

- [ ] Light theme aplica correctamente los colores de marca

- [ ] Dark theme tiene buena legibilidad (contraste WCAG AA)

- [ ] Toggle funciona y persiste entre recargas (localStorage via next-themes)

- [ ] No hay flash de tema incorrecto al cargar (SSR correcto)

- [ ] `prefers-color-scheme` del sistema se respeta en primera visita

- [ ] Transición suave entre temas (0.2s en background y color)

- [ ] ThemeToggle visible en UI con ícono correcto según tema activo

### Visual

- [ ] Light: Sidebar fondo `#423558` (dark purple)

- [ ] Light: Sidebar nav activo muestra gold (`#b7861b`)

- [ ] Light: Page background `#c5ccd5` (neutral)

- [ ] Light: Cards muestran `#f9f8fc` con borde sutil purple

- [ ] Dark: Sidebar fondo `#1a1228`

- [ ] Dark: Page background `#0f0a1a`

- [ ] Montserrat cargando vía next/font

- [ ] Sin hex raw introducidos â€” todo via tokens o brand-\*

- [ ] Sin regresión en funcionalidad â€” StatCards del dashboard renderizan

- [ ] Responsive: 375px / 768px / 1280px

### Out of Scope

- Páginas fuera del dashboard route (`/members`, `/invoices`, `/records`, `/billing`, etc.)

- Esas páginas usan `zinc-*` hardcoded y se migrarán en fases futuras
