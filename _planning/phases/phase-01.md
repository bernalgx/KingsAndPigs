# Phase 01 — Foundations & Agent Migration

## Goal

Stand up a working Planner/Coder/Auditor agent loop for the IX Unity project, adapted from the previous
BluVoid (Node/web) rules, before any gameplay or tileset work starts.

## Scope

- `.agents/rules/*.md` and `.agents/workflows/*.md` reflect Unity/C#, not npm.
- `_planning/current-task.md` and a `_planning/phases/` folder exist in a state any agent (Planner, Coder,
  Auditor, or a fresh Claude session) can pick up cold, with no unresolved placeholders.
- Design bible location resolved (see `current-task.md` open items).

## Out of scope (deferred to later phases)

- Tileset slicing / Tile Palette / Rule Tiles → Phase 02.
- Biome 1 layout/optimization planning → Phase 03.
- Any `PlayerController`/`GatherInput` code changes.

## Acceptance Criteria

- [ ] Rules files contain no leftover npm build/lint/test references.
- [ ] Coder `## Guardrails` section is filled in, not the placeholder "(i need help here)".
- [ ] `global.md` → Stack names an actual engine version, not "please fill the specifics".
- [ ] Design bible path is either resolved inside this repo or logged as an explicit blocker in
      `current-task.md`.

## Files To Read

- `.agents/rules/global.md`
- `.agents/rules/planner.md`
- `.agents/rules/coder.md`
- `.agents/rules/auditor.md`
