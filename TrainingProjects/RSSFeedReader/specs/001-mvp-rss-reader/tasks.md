# Tasks: MVP RSS Reader

**Input**: Design documents from `specs/001-mvp-rss-reader/`

**Prerequisites**: `plan.md`, `spec.md`, `research.md`, `data-model.md`, and
`contracts/subscriptions-api.md`

**Organization**: Tasks are grouped by user story so the MVP can be implemented and tested as an
independent increment.

## Phase 1: Setup

**Purpose**: Create the solution and application/test project skeletons.

- [X] T001 Create `RSSFeedReader.sln` and the .NET 10 ASP.NET Core API project at `backend/RSSFeedReader.Api/RSSFeedReader.Api.csproj`.
- [X] T002 [P] Create the .NET 10 standalone Blazor WebAssembly project at `frontend/RSSFeedReader.UI/RSSFeedReader.UI.csproj`.
- [X] T003 Add `backend/RSSFeedReader.Api.Tests/RSSFeedReader.Api.Tests.csproj` as an xUnit test project, reference the API project, and add both projects to `RSSFeedReader.sln`.

## Phase 2: Foundational

**Purpose**: Establish local configuration and cross-origin access before implementing the
subscription workflow.

- [X] T004 Configure backend and frontend local launch URLs in `backend/RSSFeedReader.Api/Properties/launchSettings.json` and `frontend/RSSFeedReader.UI/Properties/launchSettings.json` using the documented ports 5151 and 5213.
- [X] T005 Configure the frontend API base URL in `frontend/RSSFeedReader.UI/wwwroot/appsettings.json` and restrict API CORS in `backend/RSSFeedReader.Api/Program.cs` to the documented local frontend origins.

**Checkpoint**: Project skeletons and local API/frontend configuration are ready for User Story 1.

## Phase 3: User Story 1 - Add and View Feed Subscriptions (Priority: P1) 🎯 MVP

**Goal**: Let the local user add one or more URLs, see them in submission order immediately, and
reject blank submissions without fetching feeds.

**Independent Test**: Start with an empty list, add two different non-empty URLs, and verify both
appear in order. Submit an empty and a whitespace-only value and verify neither is added. Restart
the API and verify the list is empty.

### Implementation for User Story 1

- [X] T006 [P] [US1] Define the `FeedSubscription` URL model in `backend/RSSFeedReader.Api/Models/FeedSubscription.cs`; require a non-whitespace URL and do not validate reachability or feed format.
- [X] T007 [US1] Implement a thread-safe process-local subscription store in `backend/RSSFeedReader.Api/Services/InMemorySubscriptionStore.cs`; preserve submission order, accept duplicate URLs, and retain entries only until API shutdown.
- [X] T008 [US1] Add unit tests for ordered additions, duplicate acceptance, and blank-value rejection in `backend/RSSFeedReader.Api.Tests/Services/InMemorySubscriptionStoreTests.cs`.
- [X] T009 [US1] Implement `GET /api/subscriptions` and `POST /api/subscriptions` with the documented JSON shapes and 400 response for missing, empty, or whitespace-only URLs in `backend/RSSFeedReader.Api/Endpoints/SubscriptionEndpoints.cs`, then map the endpoints in `backend/RSSFeedReader.Api/Program.cs`.
- [X] T010 [US1] Add API integration tests for empty-list GET, successful POST followed by GET, and invalid POST returning 400 without adding an entry in `backend/RSSFeedReader.Api.Tests/SubscriptionsApiTests.cs`.
- [X] T011 [P] [US1] Implement a frontend client for GET and POST `/api/subscriptions` using the configured API base URL in `frontend/RSSFeedReader.UI/Services/SubscriptionApiClient.cs` and register it in `frontend/RSSFeedReader.UI/Program.cs`.
- [X] T012 [US1] Replace the starter home/demo UI with an accessible add-and-list subscription page in `frontend/RSSFeedReader.UI/Pages/Home.razor`; load the current list, display each URL, submit additions immediately, reject blank input, show actionable request failures, and remove obsolete demo navigation links.

**Checkpoint**: The single MVP story works independently, including its API contract, UI workflow,
blank-input rejection, and process-local data lifecycle.

## Phase 4: Polish & Cross-Cutting Concerns

**Purpose**: Run the documented quality gates and validate the end-to-end flow.

- [ ] T013 Run `dotnet build RSSFeedReader.sln` and `dotnet test RSSFeedReader.sln`, then complete all manual scenarios in `specs/001-mvp-rss-reader/quickstart.md` and fix any failures.

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: Starts immediately. T003 depends on T001 so it can add projects to the
  solution; T002 can run in parallel with T001.
- **Foundational (Phase 2)**: Starts after the API and frontend projects exist; completes before
  story work.
- **User Story 1 (Phase 3)**: Starts after Phase 2. It is the only user story in this MVP.
- **Polish (Phase 4)**: Starts after User Story 1 is implemented.

### User Story Dependencies

- **User Story 1 (P1)**: Depends only on Setup and Foundational work; there are no other stories.

### Within User Story 1

- T006 and T011 can run in parallel because they work in separate projects and files.
- T007 depends on T006; T008 depends on T007.
- T009 depends on T005-T007; T010 depends on T009.
- T012 depends on T011 and the API contract implementation in T009.
- T013 depends on all preceding tasks.

## Parallel Execution Examples

### Setup

```text
T001: Create solution and backend API project
T002: Create standalone Blazor WebAssembly project
```

T001 and T002 can be performed in parallel; T003 follows both.

### User Story 1

```text
T006: Define the subscription model
T011: Implement the frontend API client and registration
```

After foundational configuration, T006 and T011 can proceed in parallel. Complete backend store,
endpoints, and tests before validating the frontend workflow in T012.

## Implementation Strategy

### MVP First

1. Complete Setup and Foundational tasks.
2. Complete User Story 1 tasks.
3. Run the unit and API integration tests, then validate the browser flow in `quickstart.md`.
4. Stop after the add-and-list MVP; do not add feed fetching, parsing, persistence, deletion, or
   automatic refresh.

## Notes

- Every task uses the required checkbox, sequential ID, optional parallel marker, required story
  label for story tasks, and explicit file path.
- Automated tests are included to meet the constitution's quality gates; no test-first/TDD
  workflow is assumed.
- T013 automated build/test and HTTP scenarios passed. Interactive browser validation could not
  be run because this environment has no installed browser or browser-automation tool.
