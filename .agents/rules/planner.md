# BluVoid (IX) Planner (@planner)

## Role

Coordinate phases for the IX build. Do not write product code.

## Token Budget

The one-time full read of the design bible (`_planning/MAIN Planning/`, space not hyphen) was completed by
Planner on **2026-09-20** at Phase 1 close. Do NOT repeat it. Plan from the digest below and open a specific
bible file only when a phase needs a detail the digest does not carry. Never read
`_planning/audit_report.md` or `_planning/assets/BRAND palette.md` — both are stray content from the
unrelated jAIme web project.

Per activation, read only:

1. `_planning/current-task.md`
2. target `_planning/phases/phase-XX.md`
3. previous phase/audit only if approval or rejection details are unclear

## Design bible digest (verified 2026-09-20 — source of truth for phase splitting)

- **Game.** IX (working title "Machete Night"). 2D metroidvania in Unity 6.3 LTS (6000.3.4f1), URP 2D,
  PPU 100. Ix is a pizote (coati) who starts with only a machete, run and jump. Every gating ability and
  spell is a **Nahual** (20 total, Maya Cholq'ij order, B'atz' at 12 o'clock) that Ix awakens, never earns.
- **Theme.** "El deshilachamiento del mundo": the world is not cursed, it is stagnating because bonds broke.
  Seven Sinners = seven fractures of the bond — Negación, Control, Aislamiento, Hedonismo, Culpa, Apego,
  Permanencia (The Goat). Enemies act from fear/attachment, not evil. The Goat guides by position only and
  never states objectives; in the final act Ix frees her and she says "gracias".
- **Biome 1 — Sotobosque Nuboso** (Pérez Zeledón / Cloudbridge / Cerro de la Muerte). Home, humid, green,
  "melancolía viva", "hogar, no amenaza". Canon palette: dominants `#1F3440 #2E4A5A #2F4F4A #3E5F5A` (~80%),
  vegetation `#4F7F73 #5E8F84 #5B746F #6A8F8A`, bark/soil `#3B332C #4A4036 #2D2E2B`, fog
  `#7C9CA0 #8FAFB1 #445D63`, yellow `#D6C97A #B8A65C #E6E1B5` only in safe zones. No pure green, no pure white.
- **Demo (vertical slice) spec.** 20–35 min. Rooms: S0 Hogar → S1 Sendero Nuboso (movement tutorial) →
  HUB Claro Central (hamaca 1, The Goat first appears) → Rama A "Raíces que insisten" (dash, collapsing
  platforms, obligatory mini-boss **Guardián de Raíz** HP 180, 3 phases) / Rama B "Lo que no se ve"
  (secrets, false walls, Goat lore) / Rama C "Movimiento inestable" (air combat, optional mini-boss
  **Abejorro Ritual** HP 140) → S5 Antesala del Silencio (hamaca 2) → S6 arena **Sinner IV — La Negación**
  (faceless bark humanoid, 2 phases, environment desaturates and quietens as it loses).
- **Enemies (demo, closed scope).** Mossgrub (HP 10, ground, dies in 1 hit), Mossclad (HP 25, heavy ground,
  defense 1.8 / 1.0), Mossmir (HP 10, fast flyer, 1.2 / 1.0), Mossflutter (HP 15, erratic flyer, 1.5 / 1.1).
  Max 3 on screen; never 2 Mossclad; never 3 flyers; pre-boss room has none.
- **Damage system.** Damage comes only from the machete "Needle" table (Base 5, Lv2 9; Lv3+ locked for the
  demo). Enemies have HP plus a defense divisor per machete level: `final = machete / defense`.
- **Movement pillars (canon).** Hold-jump height, variable gravity, coyote ≈ 0.1 s, jump buffer ≈ 0.1 s,
  double jump, ground + air dash with a stamina cost. Timers, not coroutines (see `coder.md`).
- **Starter nahual.** Guacamaya / Lapa — dash + spirit missiles (1 / 3 / 5). `LapaMissile.cs` already exists.
- **Bible priorities.** Movement feel over enemy count; 6–7 polished mechanics; hamacas as checkpoints with
  music layering; NPCs (La Tejedora, El Niño Perdido) reflect emotion and never explain lore; no text-heavy
  exposition anywhere.
- **File notes.** `Documento sin título.docx` is the *superseded* early taopunk / Nine Sols concept — do not
  plan from it. `Documento sin título(1).docx` and `Paleta de colores bioma sotobosque.docx` are empty.
  `cosmovision_indigena.md` is a 2014 MEP Bribri/Cabécar teaching booklet — cultural reference only.
  `Nahuales_IX 250126.xlsx - Nahuales.csv` is the current 15-row nahual table; the 20-nahual calendar
  mapping lives in `UI de las habilidades y el arbol de guanacaste.docx` and `Espirutos animales.docx`.

## Workflow

- Activate phase: confirm previous phase is `APPROVED`, then set `current-task.md` to `IN_PROGRESS`,
  `Assigned to: Coder`.
- Resolve blocker/rejection: read the blocker/audit, add precise fixes to `current-task.md`, return to
  `IN_PROGRESS`.

## Roadmap (rewritten 2026-09-20 from the design bible — demo / vertical slice first)

Each phase gets its own `phase-XX.md` when activated; only 01 and 02 are fully written so far.
`phase-03.md` on disk is a stale placeholder and is rewritten at activation.

- **Phase 01 — Foundations.** APPROVED 2026-09-20. Rules migrated to Unity/C#; stack confirmed.
- **Phase 02 — Tileset pipeline & provisional Sotobosque palette.** Move `tileset.jpg` into
  `Assets/Tilesets/Sotobosque/`, importer preset at PPU 100, Tile Palette + terrain Rule Tile, repaint
  `Level_Prototype`. Cannot be APPROVED until Bernal delivers the production PNG (100 px cells, alpha) and
  decides palette alignment (see `phase-02.md` → Blockers).
- **Phase 03 — Core movement feel.** Audit and tune the existing `PlayerController` against the canon
  pillars (hold-jump, variable gravity, coyote 0.1 s, jump buffer 0.1 s, double jump, dash + stamina). No new
  systems; expose tunables. Manual playtest gate.
- **Phase 04 — Biome 1 greybox: S0 → S1 → HUB.** New scene `Biome1_Sotobosque`; rooms laid out with the
  Phase 02 tiles; Cinemachine confiner per room; hamaca as respawn point; thin / collapsing platforms as
  S1 needs. Ramas A/B/C stubbed as closed doors.
- **Phase 05 — Combat & damage system.** Machete Needle table (Base / Lv2), enemy HP + defense divisor,
  hit / knockback / i-frames / hitstop across `Enemy.cs` and `PlayerController`. Tunables in
  `[SerializeField]`s; no ScriptableObjects unless ≥ 3 enemies demonstrably need shared data.
- **Phase 06 — Basic enemies.** Mossgrub, Mossclad (ground); Mossmir, Mossflutter (air). Simple state
  machines, telegraph timers as specified in the bible, spawn rules (max 3 on screen).
- **Phase 07 — Rama A + mini-boss Guardián de Raíz.** Room A1 (dash + collapsing platforms) and A2 arena;
  3-phase script from the bible; root hazards.
- **Phase 08 — Atmosphere pass.** Parallax fog layers and silhouettes per palette rules, Global Light 2D
  tuning, decoration props (ferns, moss, lianas; yellow flowers only in safe zones), ambient audio layering,
  music entering at hamaca 1.
- **Phase 09 — The Goat & NPCs.** The Goat as positional guide (appears at hamaca 1, reacts to the world,
  never to Ix); La Tejedora and El Niño Perdido as silent NPCs. No dialogue system beyond what these need.
- **Phase 10 — Boss: Sinner IV La Negación.** S5 antesala + S6 arena, 2 phases, ritual attack patterns,
  environment desaturation and sound change on phase shift and on defeat.
- **Phase 11 — Ramas B/C, HUD, starter nahual, demo build.** Rama B secrets / false walls; Rama C air rooms
  (+ optional Abejorro Ritual); HUD (life, stamina, dash indicator); Lapa nahual dash + missiles wired to
  `LapaMissile.cs`; Windows build + 3–4 min capture.
- **Deferred to full game (not phases yet).** Nahual ring UI + Árbol de Guanacaste skill tree, remaining 19
  nahuales, biomes 2–7 and Sinners I–III / V–VII, Corruption / Herida del Mundo palette, localisation.

Order follows the bible's own recommendation ("S1 + HUB → Rama A + mini-boss → movement until perfect →
boss"), with movement pulled forward because Game Feel is a non-negotiable in `global.md`. Bernal may
reorder. Planner re-splits any phase whose `phase-XX.md` grows past one screen of acceptance criteria.

## Output

Update `current-task.md`. Reply:
`ACTION / STATUS / ASSIGNED TO / PHASE / NEXT`
