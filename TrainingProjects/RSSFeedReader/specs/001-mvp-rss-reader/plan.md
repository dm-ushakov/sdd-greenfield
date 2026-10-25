# Implementation Plan: MVP RSS Reader

**Branch**: `001-mvp-rss-reader` | **Date**: 2026-10-25 | **Spec**: [spec.md](./spec.md)

**Input**: Feature specification from `specs/001-mvp-rss-reader/spec.md`

## Summary

Build the MVP subscription workflow: users can submit feed URLs and see the current session's
subscription list. Use an ASP.NET Core Web API as the authoritative in-memory store and a Blazor
WebAssembly UI to submit and display subscriptions. Do not fetch or parse feeds or add persistence.

## Technical Context

**Language/Version**: C# on .NET 10 (SDK 10.0.401 is installed)

**Primary Dependencies**: ASP.NET Core Web API and Blazor WebAssembly; no feed-fetching or parsing
dependency is required for the MVP.

**Storage**: Thread-safe, process-local in-memory subscription store; data is lost when the API
process stops.

**Testing**: xUnit unit and API integration tests; manual browser verification of the local UI
workflow.

**Target Platform**: Local development on Windows, macOS, and Linux; browser-based frontend and
local API.

**Project Type**: Web application with separate backend and frontend projects.

**Performance Goals**: Adding a subscription and refreshing the visible list completes within one
second under local development conditions.

**Constraints**: Single local user; no accounts, feed requests, parsing, persistence, deletion,
or scheduled refresh. The API and frontend origins and API base URL must remain consistent with
the local configuration documented by stakeholders.

**Scale/Scope**: One user and at least 10 subscriptions per application session; one core workflow.

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

| Principle | Plan alignment |
|---|---|
| Secure by Design | MVP makes no outbound requests and renders only entered URL text; reject blank input at both UI and API boundaries. |
| Maintainable Architecture | Keep backend subscription operations and frontend presentation in separate projects; read the API base URL from configuration. |
| MVP Scope and Simplicity | Implement only add and list with volatile in-memory storage; defer fetching, parsing, persistence, deletion, and polling. |
| Verifiable Quality | Add unit/API integration tests and verify the add/list UI manually; build and test with .NET tooling. |
| Usable and Accessible Interface | Provide a named, keyboard-operable URL input and submit control, display the list, and give actionable feedback for blank input or request failure. |

**Pre-research gate**: PASS. **Post-design re-evaluation**: PASS. No constitution violations or
exceptions are required.

## Project Structure

### Documentation (this feature)

```text
specs/001-mvp-rss-reader/
├── plan.md
├── research.md
├── data-model.md
├── quickstart.md
├── contracts/
│   └── subscriptions-api.md
└── tasks.md
```

### Source Code (repository root)

```text
RSSFeedReader.sln
backend/
├── RSSFeedReader.Api/
└── RSSFeedReader.Api.Tests/

frontend/
├── RSSFeedReader.UI/
└── RSSFeedReader.UI.Tests/
```

**Structure Decision**: Use the backend/frontend split specified in `StakeholderDocuments/TechStack.md`.
Keep API contract and service tests with the backend, and UI behavior tests with the frontend.
The repository currently contains no application projects; the paths above are the intended
locations for implementation.

## Complexity Tracking

No constitution violations. No additional projects or architectural layers are proposed.
