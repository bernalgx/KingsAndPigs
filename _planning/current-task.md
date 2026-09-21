# Current Task

## Phase: 01 — Foundations & Agent Migration

## Status: REVIEW

## Assigned to: Bernal (review), then Planner (activate Phase 2)

## Context

Migrating the Planner/Coder/Auditor agent setup from the previous BluVoid web project (jAIme) into this
Unity project (IX). The original rules referenced `npm run build/lint/test` and a design-bible path on a
different machine — both were blocking issues for a fresh agent session working in this repo.

## Done this phase

- Rewrote `.agents/rules/global.md`, `planner.md`, `coder.md`, `auditor.md` for Unity/C#/IX instead of
  Node/web.
- Replaced npm-based validation everywhere with a Unity-appropriate flow: static self-check → optional
  `dotnet build` → explicit ask for a clean Unity recompile / Console check.
- Filled in the Coder `## Guardrails` section (was empty) using IX's actual design pillars: no
  overengineering, no magic numbers, no per-frame allocations, timers (not coroutines) for coyote
  time/jump buffering, and an explicit edge-case checklist folded into every handoff.

## Open before this phase can be marked APPROVED

- [x] Confirm the actual Unity version — **Unity 6 LTS (6000.3.4f1)**. Recorded in `global.md` → Stack.
- [x] Confirm PPU — **100**. Recorded in `global.md` → Stack.
- [ ] Confirm scripting backend (Mono vs IL2CPP) in Project Settings; record in `global.md` → Stack.
- [ ] Copy/point the design bible (previously
      `C:\Users\mau\source\repos\KingsAndPigs_planning\MAIN Planning` on a different machine) into this
      repo at `_planning/MAIN-Planning/`, or log its real location in `global.md` → Files. Planner cannot
      do its Phase-1 read until this resolves.
