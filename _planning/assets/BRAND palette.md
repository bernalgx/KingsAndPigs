# jAIme — Color Palette
# Fuente de verdad para todos los colores.
# Todos los agentes deben leer este archivo antes de tomar decisiones de diseño.

---

## 🎨 Palette Overview

| Rol | Nombre | Hex | Uso |
|---|---|---|---|
| **Dominant** | Purple | `#6f5b98` | Brand, sidebar borders, accents, badges |
| **CTA / Accent** | Gold | `#b7861b` | Botones, CTAs, nav activo |
| **Dark Neutral** | Dark Purple | `#423558` | Texto body, headings, sidebar bg |
| **Light Neutral** | Blue Gray | `#c5ccd5` | Fondos de página, surfaces grandes |
| **Purple Variant** | Light Purple | `#766096` | Hovers, elementos secundarios |

---

## 📐 Tailwind Config

```js
// tailwind.config.js
colors: {
  brand: {
    purple:  '#6f5b98',
    gold:    '#b7861b',
    lgold:   '#d4a535',
    dgold:   '#8a6414',
    dpurple: '#423558',
    lpurple: '#766096',
    neutral: '#c5ccd5',
    surface: '#f9f8fc',
  }
}
```

## CSS Variables

```css
:root {
  --color-dominant:  #6f5b98;
  --color-cta:       #b7861b;
  --color-cta-light: #d4a535;
  --color-cta-dark:  #8a6414;
  --color-text:      #423558;
  --color-secondary: #766096;
  --color-bg:        #c5ccd5;
  --color-surface:   #f9f8fc;
}
```

---

## Dashboard SaaS — Aplicación por elemento

| Elemento UI | Token |
|---|---|
| Sidebar background | `brand-dpurple` |
| Sidebar nav activo | `brand-gold` |
| Sidebar nav inactivo | `brand-neutral` opacity-70 |
| Topbar | white + border `brand-purple/10` |
| Page background | `brand-neutral` |
| Cards | white / `brand-surface` |
| Buttons primarios | `brand-gold` → `brand-lgold` gradient |
| Buttons secundarios | border `brand-purple`, text `brand-purple` |
| Badges status info | `brand-purple` tint |
| Badges status warning | `brand-gold` tint |
| Destructive | red — única excepción a la paleta |

---

## ✅ Reglas

- **Gold = solo para acciones clickeables** — botones, CTAs, links activos
- **Dark Purple = todo el texto** — nunca usar dominant purple para body text
- **Sin hex raw** — siempre tokens `brand-*` de Tailwind
- **Sin colores nuevos** sin actualizar este archivo primero
