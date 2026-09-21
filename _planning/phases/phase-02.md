# Phase 02 — Tileset Pipeline & Provisional Sotobosque Palette

Requires Phase 01 `APPROVED` — re-verified by Planner on 2026-09-20 against the actual repo state.

## Goal

Replace the placeholder `GroundPalette` (King Pig sprites used as bricks) in `Level_Prototype` with a real,
repeatable tileset pipeline for Biome 1 — Sotobosque Nuboso — at **PPU 100**, so that when Bernal's
production tile art lands it drops into place without redoing any setup.

## Grounding (read before touching anything)

### What the design bible says that matters for tiles

- Biome 1 is Ix's home: humid, green, alive but strangely still ("melancolía viva", "herido, no muerto").
  It must NOT read as ruined or hostile. "Este bioma es hogar, no amenaza."
- Canon palette (`Paleta de Colores Sotobosque bioma 1.docx` / `🎨 Paleta de colores — Bioma inicial.docx`):
  - Dominants, ~80% of the biome: `#1F3440` sombra viva, `#2E4A5A` humedad, `#2F4F4A` verde nuboso,
    `#3E5F5A` verde frío.
  - Vegetation: `#4F7F73` musgo, `#5E8F84` helechos, `#5B746F` arbustos/pasto, `#6A8F8A` lianas/epífitas.
  - Bark / roots / soil: `#3B332C`, `#4A4036`, `#2D2E2B`.
  - Fog / parallax: `#7C9CA0` niebla frontal, `#8FAFB1` fondo lejano, `#445D63` siluetas.
  - Yellow accents `#D6C97A` / `#B8A65C` / `#E6E1B5` ONLY in safe zones (hamacas).
  - Rules: no pure greens, no pure white, everything dirtied with blue/grey; farther = colder + greyer;
    foreground greener, background bluer.
- Flora research (`Flora montañosa de Pérez Zeledón (Bosque Nuboso).pdf`): moss on every surface, tree
  ferns, epiphytes/bromeliads, hanging lianas, Quercus costaricensis oaks, begonias, small yellow ground
  flowers. That is the prop list for future decoration tiles — not this phase.
- Terrain as narrative (`Canon oficial.docx` → Bioma 1): "plataformas naturales que no conectan bien",
  "lianas que llegan, pero no alcanzan". Gaps and ledges are a device, so platform/ledge tiles matter.
- Demo map (`BIOMA INICIAL — SOTO BOSQUE NUBOSO hogar de ix.docx`): S0 Hogar → S1 Sendero Nuboso → HUB
  Claro Central → ramas A/B/C → S5 Antesala → S6 arena. Phase 02 does NOT build rooms; it makes the tiles
  they will be painted with (rooms are Phase 04).

### What `tileset.jpg` actually is (Planner inspection, 2026-09-20)

- 458 × 1024 JPEG, no alpha channel, a 5-column × ~11-row grid with **white gridlines baked into the
  pixels**. Cells are ≈ 91.6 px wide × 93.1 px tall — non-integer and non-square.
- Style: flat/vector stone-and-circuit ruins in teal, navy, salmon pink and saturated blue, with bright
  green palms and lamps. It reads as ruins/tech (the earliest "Machete Night" taopunk concept), not as the
  organic cloud-forest palette above. Pinks, saturated blues and pure greens break the canon palette rules.
- Approximate inventory (Coder confirms when slicing; rows counted top → bottom):

  | Rows | Content | Category |
  |---|---|---|
  | 1 | 5 square stone tiles (teal ×2, grey-lilac ×3) with roots + gem accents | terrain fill / wall |
  | 2–3 | three 1×2 pink-framed teal circuit wall panels; pink ledge edge at right | struct wall / ledge |
  | 4 | 2 small hanging lattice platform pieces (right) | platform |
  | 5 | 3 thin one-way platform strips (left); 2 lattice/truss tiles (right) | platform / struct |
  | 6–7 | large rounded 2×2 frame (left); 3 cross/pipe connectors with blue accents (right) | struct |
  | 8 | 3–4 vent/grate tiles; 2 inner-corner tiles with pink/blue accents | struct |
  | 9 | empty | — |
  | 10–11 | palms ×3, glyph panel, lamp + light cone, step, fern, blue door frame, crystal pillar, cage | deco props |

- Conclusion: this is a **concept sheet**, not a production tileset. It cannot be sliced cleanly at PPU 100
  (cells aren't 100 px, gridlines will show as seams, decorations carry white backgrounds because JPG has
  no alpha). It IS good enough to prove the pipeline end-to-end and to let Bernal judge the look in-engine.

## Scope

1. **Housekeep the source.** `git mv tileset.jpg Assets/Tilesets/Sotobosque/Reference/tileset_concept_v0.jpg`
   (create the folders). Nothing may stay at repo root. Import as Sprite (2D and UI), Sprite Mode Single,
   PPU 100 — it is a reference image, not a palette source (step 4 is the provisional exception).
2. **Folder + naming convention** under `Assets/Tilesets/Sotobosque/`: `Reference/`, `Textures/`, `Tiles/`
   (generated Tile assets + Rule Tiles), `Palettes/`. Tile assets are named `sb_<category>_<name>` with
   category ∈ {terrain, platform, struct, deco}.
3. **Importer preset** `Textures/TileTexture.preset` (Unity Preset for TextureImporter): Texture Type
   Sprite (2D and UI), Sprite Mode Multiple, **Pixels Per Unit 100**, Filter Mode Point (no filter),
   Compression None, Mesh Type Full Rect, Generate Mip Maps off, Wrap Mode Clamp, Read/Write off. Every
   future tile texture in this folder gets this preset applied.
4. **Provisional slice** of the concept sheet — the ONE sanctioned PPU deviation. Duplicate the reference as
   `Textures/sb_concept_provisional.png` (PNG so later edits can carry alpha). Sprite Editor → Slice →
   Grid By Cell Count, 5 columns × 11 rows. Set THIS texture's PPU so one cell ≈ 1 world unit (≈ 92; record
   the exact value in the handoff). This texture and its tiles are placeholders: they are deleted when the
   production PNG lands and may be referenced only by the palette and `Level_Prototype`.
5. **Tile Palette** `Palettes/SotobosquePalette.prefab`, Grid cell size 1 × 1 (matches the scene Grid),
   Cell Sizing Manual. Arrange by category: terrain, platform, struct, deco. Do not delete `GroundPalette`;
   it simply stops being used.
6. **Rule Tile** `Tiles/sb_terrain_rule.asset` (2D Tilemap Extras 6.0.1 is already in `Packages/manifest.json`)
   for ground/wall auto-tiling with whatever variants the sheet provides (minimum: fill + top edge + two top
   corners). Every rule you could NOT satisfy with the source goes in the handoff as "missing tiles" — that
   list is the art request for Bernal.
7. **Wire into `Level_Prototype`.** Repaint the `Ground` tilemap's playable area with `sb_terrain_rule` and
   the platform tiles; repaint `WallBackGround` with struct/wall panels where the King Pig bricks are today.
   `Ground` keeps: GameObject layer `Ground` (index 7), `TilemapCollider2D` + `CompositeCollider2D` (Used By
   Composite on), sorting layer `Ground`. `WallBackGround` stays on sorting layer `BackGround`, no collider.
   `Decorations` (sorting layer `MiddleGround`) may receive a few deco tiles but is not required this phase.
8. **Handoff** per `coder.md`, plus: the tile inventory as actually sliced, the missing-tiles list, the
   provisional PPU value, B1/B2 status, and a one-line ask to Bernal for the manual playtest.

## Out of scope (do not drift)

- Any `.cs` change. If repainting breaks ground/wall detection, stop and report — do not touch
  `PlayerController.groundLayer`, ray lengths, or layers.
- One-way platforms (`PlatformEffector2D`), moving/collapsing platforms → Phase 04.
- Parallax fog layers, Global Light 2D tuning, palette-driven post → Phase 08.
- Decorations as prefabs/animated props (palms, lamp cones, ferns) → Phase 08.
- New biome scene or room layout (S0/S1/HUB) → Phase 04. Stay in `Level_Prototype`.
- Pixel Perfect Camera / Cinemachine changes.
- Recolouring the art to the canon palette — Bernal's decision (see Blockers), not a Coder task.

## Blockers / decisions only Bernal can make (log answers in `current-task.md`)

- **B1 — Production tileset PNG.** Needed before this phase can be `APPROVED`: PNG with alpha, no gridlines,
  uniform square cells of **100 × 100 px** (1 tile = 1 unit at PPU 100 with the existing 1×1 Grid). If Bernal
  prefers smaller tiles (e.g. 50 px), say so — then Grid cell size changes to 0.5 in the same change and the
  decision is recorded in `global.md` → Stack. The Rule Tile needs at least: fill, top, bottom, left, right,
  4 outer corners, 4 inner corners. Nice to have: 45° slopes, thin platform left/middle/right.
- **B2 — Palette alignment.** `tileset.jpg` (pink/blue/teal ruins) vs the canon Sotobosque palette
  (blue-green organic forest). Options: (a) this sheet belongs to a later ruins area / Herida del Mundo and
  Biome 1 needs its own organic set; (b) recolour this sheet to the canon hexes; (c) accept it as Biome 1
  and amend the palette doc. Planner recommends (a) or (b) — the bible is unambiguous that Biome 1 is
  "hogar, no amenaza". Seeing the provisional slice in-engine (step 7) is the fastest way to decide.

## Acceptance Criteria

- [ ] No `tileset.jpg` at repo root; the concept sheet lives under `Assets/Tilesets/Sotobosque/Reference/`.
- [ ] Folder structure + naming convention exist as specified; `TileTexture.preset` exists and is applied to
      every tile texture in `Textures/`.
- [ ] `SotobosquePalette` exists, cell 1×1, categorised, and contains every non-empty cell of the sheet.
- [ ] `sb_terrain_rule` Rule Tile exists and paints fill + top edge + top corners correctly on `Ground`.
- [ ] `Level_Prototype` `Ground` and `WallBackGround` no longer reference any `Terrain (32x32)_*` /
      `GroundPaletteTiles` tile in the playable area; collider setup, GameObject layers and sorting layers
      unchanged.
- [ ] Zero `.cs` diffs.
- [ ] Bernal playtest note recorded: Ix lands, walks, wall-slides and jumps on the new tiles exactly as before.
- [ ] Handoff contains: sliced inventory, missing-tiles list, provisional PPU value, B1/B2 status.
- [ ] `APPROVED` additionally requires B1 delivered and swapped in (provisional texture deleted) — or an
      explicit Bernal decision, recorded in `current-task.md`, to greybox the demo on the provisional tiles.

## Files To Read

- `_planning/current-task.md`
- `.agents/rules/global.md` → Stack, Current Scene State, Files
- `Assets/Scenes/Level_Prototype.unity` — only the `Grid`, `Ground`, `WallBackGround`, `Decorations`
  objects (`rg -n "m_Name: (Grid|Ground|WallBackGround|Decorations)"`, read ±60 lines). Known: Grid cell
  size 1×1; `Ground` is layer 7 with `TilemapCollider2D` + `CompositeCollider2D`.
- `Assets/Tilemaps/Palettes/GroundPalette.prefab` — the pattern being replaced (cell 1×1, layout 0).
- `Assets/Sprites/14-TileSets/Terrain (32x32).png.meta` — importer field names only; its PPU 32 is the
  tutorial's, NOT ours.
- `Assets/Scripts/PlayerController.cs` lines 100–110 and 375–410 (read-only: `groundLayer` foot raycasts
  and wall raycast — this is what the repainted tiles must keep satisfying).
- `Packages/manifest.json` — confirms `com.unity.2d.tilemap.extras` 6.0.1 (Rule Tile) is present.
