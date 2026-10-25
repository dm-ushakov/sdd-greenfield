# Research: MVP RSS Reader

## Decision: Use the .NET 10 LTS release line

**Rationale**: The repository has no existing application target to preserve, the installed SDK
is 10.0.401, and Microsoft lists .NET 10 as the supported LTS release for this new project.
Use the latest available .NET 10 servicing patch when implementing.

**Alternatives considered**: Target an older runtime for compatibility; not selected because no
older target is stated by the project and .NET 10 is available locally.

**Source**: [Microsoft .NET and .NET Core Support Policy](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core)

## Decision: Keep the API process as the authoritative in-memory store

**Rationale**: The stakeholder goals require subscriptions only for the current run and defer
persistence. A singleton in-memory service allows the frontend to add subscriptions through the
API and retrieve the same list for the lifetime of the local API process. The service must be
thread-safe because it is shared across requests. Restarting the API clears the list; multi-process
sharing is outside this single-user MVP.

**Alternatives considered**: Browser-only storage would make the UI authoritative and diverge from
the stakeholder architecture assigning subscription data to the backend. Durable storage is out
of scope.

**Source**: [Microsoft .NET dependency-injection service lifetimes](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection/service-lifetimes)

## Decision: Validate required input at both UI and API boundaries

**Rationale**: UI feedback helps users correct a blank submission, while server-side validation
ensures invalid requests cannot bypass the interface. This MVP checks only for a non-empty,
non-whitespace URL; it does not check reachability, feed format, or make network requests.

**Alternatives considered**: UI-only validation is insufficient for an API that can receive
requests independently. Feed URL validation would exceed the approved MVP scope.

**Sources**:
[Blazor form validation](https://learn.microsoft.com/en-us/aspnet/core/blazor/forms/validation?view=aspnetcore-10.0);
[ASP.NET Core model validation](https://learn.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-10.0)

## Decision: Use focused .NET build and test gates

**Rationale**: Build the solution to catch compile errors and run unit/API integration tests to
verify the service and HTTP contract. Manually verify the browser workflow because the repository
does not yet contain browser automation infrastructure.

**Alternatives considered**: Browser end-to-end automation could be added later if the project
establishes that tooling; it is not required to deliver this small MVP.

**Sources**:
[dotnet build](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-build);
[dotnet test](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-test)

## Decision: Use the documented local ports and restrict CORS

**Rationale**: Stakeholder documentation identifies the API at `http://localhost:5151` and the
frontend at `http://localhost:5213` (with the documented HTTPS frontend origin where applicable).
Use those values consistently in launch settings and frontend configuration, and allow only the
required local frontend origin(s) through CORS.

**Alternatives considered**: Arbitrary ports or permissive CORS would diverge from project
documentation and make local setup less predictable.
