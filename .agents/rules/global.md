# BluVoid Agent Rules — IX (Metroidvania 2D)

## Product

IX — 2D Metroidvania in Unity, originally started from a "Kings and Pigs" tutorial and now diverging into
an original game. Solo/indie project by Bernal (BluVoid Software), built with AI coding agents in
Antigravity split into three roles: Planner, Coder, Auditor.

## Token Budget

- Read in this order: `_planning/current-task.md`, active `_planning/phases/phase-XX.md`, then only files
  named in the phase/current task.
- Do not bulk-read `_planning`, audits, reports, or whole directories unless needed to resolve a concrete
  blocker.
- Prefer `rg`/grep for targeted search inside `Assets/Scripts`. Open only matching files/sections.
- Summarize findings; do not paste long source text into handoffs.

## Non-Negotiables

- Next phase requires previous phase `APPROVED`.
- Planner plans, Coder implements, Auditor verifies. No role skips ahead or does another role's job.
- **Anti-overengineering.** Don't add managers, interfaces, ScriptableObjects, or event systems unless the
  active module demonstrably needs one. The simplest solution that keeps responsibilities separated wins
  over the "correct" academic pattern.
- **Zero magic numbers** in gameplay code — expose via `[SerializeField]`/`[Range]` or a named constant.
- **Zero per-frame allocations** in `Update`/`FixedUpdate` — cache components, cache
  `Animator.StringToHash` ids, reuse buffers, avoid LINQ in hot paths.
- Every exposed Inspector field gets `[Header]`/`[Tooltip]`; hard dependencies get `[RequireComponent]`.

(Dropped: the old "follow existing i18n patterns" line from the previous project's rules — that was about
a web SaaS with copy/localization concerns and doesn't apply to this repo. If IX ever needs in-game text
localization, add a fresh rule for it then, don't resurrect this one.)

## Stack

- Engine: **Unity 6 LTS (6000.3.4f1)**, DX12 in-editor. Confirmed.
- Language: C#, scripting backend as configured in Project Settings (confirm Mono vs IL2CPP during Phase 1
  — not yet confirmed).
- Rendering: URP 2D. Pixel Perfect Camera + Cinemachine 2D virtual cameras once camera work starts (a
  `CinemachineCamera` already exists in `Level_Prototype`).
- Input: new Input System — `Assets/Inputs/Controls.inputactions` (generated `Controls.cs`), read through
  `GatherInput.cs`.
- Physics: `Rigidbody2D` + `Physics2D` raycasts/overlaps and a custom `PlayerController` state machine — not
  Unity's built-in `CharacterController`.
- Target: PC/console, 60 FPS, pixel art. **PPU: 100. Confirmed.**
- Current source-of-truth scripts (treat as the real API surface until a phase explicitly rewrites them):
  `PlayerController.cs`, `GatherInput.cs`, `Controls.cs`, `Enemy.cs`, `GameManager.cs`,
  `CustomCameraOffset.cs`, `LapaMissile.cs`.

## Validation

This is a Unity project, not a Node/web app — there is no `npm run build/lint/test` here, even though that
was the default in the rules this setup was migrated from. Use instead, in order of what's actually
available in the agent's environment:

1. Static self-check: does the diff compile — correct `using`s, matching signatures, no dangling
   references to renamed fields/methods?
2. If Unity has generated a `.csproj` for the project and `dotnet` is reachable, `dotnet build` against it
   as a fast syntax/reference check.
3. Otherwise, end the pass with an explicit ask: "open Unity, let it recompile, paste back any Console
   errors/warnings" — before a phase is marked `APPROVED`, not after.
4. For Game Feel changes (coyote time, jump buffering, wall jump, dash i-frames, hitstop) — no automated
   check replaces a one-line manual playtest note from Bernal. Ask for it explicitly rather than assuming
   it feels right.

## Current Scene State (`Level_Prototype.unity`, as of Phase 1)

- Hierarchy: `GameManager`, `Main Camera`, `Global Light 2D`, `Player`, `Grid` (`Ground`,
  `WallBackGround`, `Decorations`), `CinemachineCamera`, `CameraOffsetZone`, `CameraLimit`, `Enemy`
  through `Enemy (3)`.
- The `Ground` tilemap is currently painted with a `GroundPalette` built from the `02-King Pig` sprite
  sheet (character art reused as placeholder brick tiles) — this is a prototyping stand-in, not the real
  tileset. Phase 2 replaces this palette with proper tiles once Bernal's actual tileset image is sliced at
  PPU 100.
- `PlayerController`'s Animator already exposes `speed`, `isGrounded`, `isWallDetected`, `Knockback`,
  `isAttacking`, `isDashing` as parameters — treat these as the existing contract, don't rename without a
  phase calling for it.

## Files

- Task: `_planning/current-task.md`
- Phase: `_planning/phases/phase-XX.md`
- Audit: `_planning/audits/audit-XX.md` (kept terse — mainly for other agent sessions, not prose for humans)
- Design bible: the previous rules pointed Planner at
  `C:\Users\mau\source\repos\KingsAndPigs_planning\MAIN Planning`, a path on a different machine that won't
  resolve inside this repo for any agent. Phase 1 must either copy that content into
  `_planning/MAIN-Planning/` inside this repo, or log its real location here once decided.
