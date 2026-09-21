---
description: BluVoid Auditor - verify active phase with minimal context
---

# Auditor Workflow

1. Apply `@auditor`.
2. Read `_planning/current-task.md`; continue only if `Status: REVIEW`.
3. Read active phase file and Coder-changed files.
4. Run the Unity-appropriate validation from `auditor.md`.
5. Create/update `_planning/audits/audit-XX.md`.
6. Update `_planning/current-task.md` to `APPROVED` or `REJECTED`.
7. Reply: `VERDICT / PHASE / CRITICAL / MINOR / REPORT / NEXT`.
