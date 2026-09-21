# Current Task

## Phase: 02 — Tileset Pipeline & Provisional Sotobosque Palette

## Status: IN_PROGRESS

## Assigned to: Coder

## Context

Phase 1 was independently re-verified by Planner on 2026-09-20 (log below) and stays `APPROVED`. The
design bible was read in full; its digest and an 11-phase roadmap now live in `.agents/rules/planner.md`.
The Phase 2 spec was fully rewritten in `_planning/phases/phase-02.md` — read it first. This file only
carries working state.

## Phase 1 verification log (Planner, 2026-09-20)

- npm references: appear only as explicit "No `npm run build/lint/test`" disclaimers in `global.md`,
  `coder.md`, `auditor.md` and `coder-workflow-v2.md`. No leftover instructions. OK.
- `coder.md` → Guardrails: filled with 7 concrete rules, placeholder gone. OK.
- Stack vs ProjectSettings: `ProjectVersion.txt` = 6000.3.4f1 (matches). `scriptingBackend` map only sets
  `Android: 1` (IL2CPP), so Standalone falls to Unity's default Mono (matches). PPU 100 on 77 of 91 sprite
  `.meta` files; the 14 at PPU 32 are the tutorial's `01-King Human` sheets and `14-TileSets`
  Terrain/Decorations (32x32) — exactly the tiles Phase 2 supersedes. OK.
- `_planning/MAIN Planning/`: 21 files with real content (canon, biome, palettes, nahuales, flora). Two
  empty docx noted in `planner.md`. OK.
- `_planning/audit_report.md`: still present (4.7 KB, dated Mar 28, jAIme RAG/Supabase audit). Not touched.
  Also `_planning/assets/BRAND palette.md` is titled "jAIme — Color Palette" — same stray origin.

## Coder — do in this order

1. `git mv tileset.jpg "Assets/Tilesets/Sotobosque/Reference/tileset_concept_v0.jpg"` (create the
   folders). Unity generates the `.meta` on next open — ask Bernal to open Unity once after the move so
   metas exist before the palette is built.
2. Folder tree (`Reference/ Textures/ Tiles/ Palettes/`) + `Textures/TileTexture.preset`
   (Sprite Multiple, PPU 100, Point, Compression None, Full Rect, no mipmaps, Clamp).
3. Provisional PNG copy → Sprite Editor Grid By Cell Count 5 × 11 → PPU ≈ 92 on that texture only.
   Record the exact value.
4. `Palettes/SotobosquePalette.prefab` (cell 1×1, categorised) + `Tiles/sb_terrain_rule.asset` Rule Tile.
5. Repaint `Ground` + `WallBackGround` in `Level_Prototype`; confirm collider, GameObject layer 7 and
   sorting layers unchanged.
6. Handoff per `coder.md` + sliced inventory, missing-tiles list, provisional PPU value, B1/B2 status.
   Zero `.cs` diffs.

Palette / Rule Tile authoring is Unity Editor work. If the Editor cannot be driven from the agent's
environment: do steps 1–2 fully, write the exact Sprite Editor → Tile Palette → Rule Tile click-path for
Bernal in the handoff using the asset names/paths above, and set Status `REVIEW` with
"Editor steps pending Bernal".

## Open asks for Bernal

- **B1** Production tileset PNG: 100 × 100 px cells, alpha, no gridlines; minimum tile set listed in
  `phase-02.md` → Blockers. Until it lands Phase 2 can reach `REVIEW` but not `APPROVED`.
- **B2** Palette alignment: `tileset.jpg` is a pink/blue/teal ruins concept sheet; the canon Biome 1 palette
  is blue-green organic forest ("hogar, no amenaza"). Decide (a) different area / (b) recolour / (c) accept.
  Recommended: decide after seeing the provisional slice in-engine.
- Playtest note after the repaint: Ix lands, walks, wall-slides and jumps as before.

## Housekeeping (carried over, not blocking)

- [ ] `_planning/audit_report.md` — stray jAIme content, still present. Move or delete.
- [ ] `_planning/assets/BRAND palette.md` — stray jAIme content (title + Tailwind tokens). Move or delete.
- [x] `tileset.jpg` at repo root — now Phase 2 step 1.
- [ ] `_planning/phases/phase-03.md` is a stale placeholder vs the new roadmap (03 is now Core movement
      feel). Planner rewrites it at activation.
