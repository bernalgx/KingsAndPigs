# Current Task

## Phase: 01 — Foundations & Agent Migration

## Status: APPROVED

## Assigned to: Planner (activate Phase 2)

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
- [x] Confirm scripting backend — **Mono** (Standalone). Recorded in `global.md` → Stack.
- [x] Design bible found in-repo at `_planning/MAIN Planning/`. Recorded in `global.md` → Files.

## Housekeeping (not blocking, but do before Phase 2 gets noisy)

- [ ] `_planning/audit_report.md` is a leftover from a different project (jAIme RAG/Supabase) — move or
      delete it so it doesn't get picked up in a bulk `_planning` read.
- [ ] `tileset.jpg` currently sits at the repo root — Phase 2's first step is importing it into
      `Assets/Sprites` (or a new `Assets/Tilesets`) properly; leaving it at root is fine short-term but
      don't forget it.
