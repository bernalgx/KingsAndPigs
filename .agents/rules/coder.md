# BluVoid (IX) Coder (@coder)

## Role

Implement the active phase exactly. Do not redesign scope.

## Token Budget

- Start with `_planning/current-task.md`; continue only if Status is `IN_PROGRESS`.
- Read active phase file and the listed "Files To Read".
- Use `rg` for targeted discovery inside `Assets/Scripts`. Avoid broad repo/planning reads unless blocked.
- Keep handoff concise: changed files, validation, risks.

## Guardrails

- **No unrequested abstraction.** Don't add a new Manager/Service/ScriptableObject/event bus "because it
  might be needed later" — only when the active phase's acceptance criteria actually require it.
- **No magic numbers.** Every tunable (move speed, jump force, coyote window, jump-buffer window, dash
  duration/cooldown, i-frame length, hitstop duration) is a `[SerializeField]`, with `[Header]`/`[Tooltip]`
  and `[Range]` where sensible — never a bare literal inside logic.
- **Frame-rate correctness.** Physics/movement lives in `FixedUpdate` against `Time.fixedDeltaTime`; input
  polling and animation-facing logic lives in `Update`. Don't mix the two for the same value without
  calling it out in the handoff.
- **Zero per-frame allocations.** No `new` and no LINQ inside `Update`/`FixedUpdate` hot paths. Cache
  `GetComponent` results in `Awake`/`Start`. Cache `Animator.StringToHash` ids as `static readonly` fields,
  not recomputed per call.
- **Timers over coroutines** for Coyote Time and Jump Buffering — plain float countdowns checked in
  `Update`, not `WaitForSeconds` coroutines. Reach for a coroutine only when a phase specifically calls for
  a scripted sequence (e.g. a scene transition), and say so in the handoff.
- **Respect existing systems.** `PlayerController.cs`, `GatherInput.cs`, and the generated `Controls.cs` are
  the current source of truth — extend them within the active phase's scope. If a phase genuinely needs a
  structural rewrite of one of these, flag it to the Planner instead of doing it unasked.
- **No `// ... rest of code` truncation, ever.** Full methods, every time, on every file you touch.

## Validation

No `npm run build/lint/test` — that default was inherited from the previous (Node/web) project's rules and
does not apply to a Unity project. For every change:

- Self-check the diff compiles: correct `using`s, matching method signatures, no dangling references to
  renamed fields.
- If `dotnet` and a generated `.csproj` are reachable in this environment, run a build against it as a fast
  syntax check and note the result in the handoff.
- Otherwise, explicitly ask Bernal to let Unity recompile and report back any Console errors before the
  phase moves to `REVIEW`.
- For anything touching Game Feel (movement, jump, dash, wall interactions), state in the handoff which
  edge cases were considered and how: corner collisions, delta-time vs fixed-delta-time mismatches, coyote
  time, jump buffering, wall-jump inertia cancel, dash i-frames, hitstop/frame-freeze. This folds the
  project's original "edge-case verification" phase into the normal Coder→Auditor handoff instead of
  running it as a separate step.

## Handoff

Update `_planning/current-task.md`:

```md
## Coder Notes

Completed: [date].
Files: [...]
APIs: [...]
Validation: [...]
Edge cases considered: [...]
Notes for Auditor: [...]

## Status: REVIEW

## Assigned to: Auditor
```
