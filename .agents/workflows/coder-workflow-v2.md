---
description: BluVoid (IX) Coder - implement active phase with minimal context
---

# Coder Workflow

1. Apply `@coder`.
2. Read `_planning/current-task.md`; continue only if `Status: IN_PROGRESS`.
3. Read active phase file and listed "Files To Read".
4. Implement only the active phase scope.
5. Run the Unity-appropriate validation from `coder.md` (self-check, optional `dotnet build`, or an
   explicit ask for a clean Unity recompile) — not npm build/lint/test.
6. Update `_planning/current-task.md` to `Status: REVIEW`, `Assigned to: Auditor`.
7. Reply: `ACTION / FILES CHANGED / VALIDATION / NOTES FOR AUDITOR / STATUS`.
