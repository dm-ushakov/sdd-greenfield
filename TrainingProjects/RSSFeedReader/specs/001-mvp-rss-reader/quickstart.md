# Quickstart: MVP RSS Reader Validation

## Prerequisites

- .NET 10 SDK
- A modern web browser
- The backend and frontend projects created at the paths in `plan.md`

## Run

1. Start the backend from the repository root:

   ```sh
   dotnet run --project backend/RSSFeedReader.Api
   ```

2. Start the frontend in a second terminal:

   ```sh
   dotnet run --project frontend/RSSFeedReader.UI
   ```

3. Open the frontend URL reported by the run command. Confirm that the configured API base URL
   points to the backend origin and that backend CORS permits the frontend origin.

## End-to-end scenarios

1. With an empty list, enter `https://example.com/feed.xml` and submit. The URL appears in the
   list without a page restart or manual refresh.
2. Enter `https://example.org/another-feed.xml` and submit. Both URLs remain visible in order.
3. Submit an empty or whitespace-only value. No item appears and the interface identifies that a
   URL is required.
4. Submit a non-empty but unreachable URL. It is added without a network request or feed check.
5. Stop and restart the backend. The list is empty, confirming that subscriptions are
   process-local and not persisted.

## Automated validation

From the repository root, after the solution and tests exist, run:

```sh
dotnet build RSSFeedReader.sln
dotnet test RSSFeedReader.sln
```

Expected result: the solution builds, all automated tests pass, and the manual scenarios above
produce the described outcomes. The API request/response shapes are documented in
[contracts/subscriptions-api.md](./contracts/subscriptions-api.md); entity constraints are in
[data-model.md](./data-model.md).
