<!--
Sync Impact Report
Version change: unversioned scaffold → 1.0.0 (first project-specific constitution)
Modified principles: scaffold placeholders → Secure by Design; Maintainable Architecture;
  MVP Scope and Simplicity; Verifiable Quality; Usable and Accessible Interface
Added sections: Technical Constraints; Development Workflow and Quality Gates
Removed sections: none
Follow-up TODO: confirm the original ratification date.
-->

# RSS Feed Reader Constitution

## Core Principles

### I. Secure by Design
All application changes MUST use safe handling for user-provided data and MUST avoid exposing
secrets, personal data, or internal details in logs and error responses. In the MVP, subscription
URLs are stored and displayed without network access. Before any feature fetches a user-provided
URL, it MUST validate the scheme and destination, prevent access to loopback, private, and
link-local addresses, and re-check destinations after redirects to mitigate server-side request
forgery. Feed content MUST be treated as untrusted; HTML MUST NOT be rendered without a
maintained sanitizer. Security controls are required because feed URLs and feed content may be
controlled by external users.

### II. Maintainable Architecture
The application MUST preserve the separation between the ASP.NET Core Web API backend and the
Blazor WebAssembly frontend. Backend responsibilities include subscription data and, when in
scope, feed operations; the frontend is responsible for user interaction and presentation.
Configuration such as API endpoints MUST be externalized rather than hard-coded in application
logic. New dependencies MUST have a clear need and MUST be compatible with the supported .NET
toolchain. These boundaries enable incremental growth without replacing the MVP architecture.

### III. MVP Scope and Simplicity
The MVP is limited to adding a feed subscription by URL and displaying the subscription list.
MVP implementations MUST NOT add feed fetching, feed parsing, persistence, deletion, or background
polling unless a separately approved feature explicitly expands scope. In-memory storage and
minimal UI are acceptable for this proof of concept. MVP URLs need not be verified as live feeds
while the application performs no network access. Every change MUST choose the simplest design
that satisfies an approved requirement; future possibilities alone do not justify implementation.

### IV. Verifiable Quality
Changes MUST build with the repository's supported .NET tooling. Automated tests MUST cover
changed behavior at the appropriate level: unit tests for isolated logic and integration tests
for API contracts or backend/frontend communication. User-visible behavior MUST be manually
verified when it cannot be reliably covered by automation. A change MUST NOT be considered
complete when its required build or tests fail; any justified exception MUST be documented in
the change review.

### V. Usable and Accessible Interface
The subscription workflow MUST allow a user to enter a URL and see the updated subscription list.
Interactive controls MUST have accessible names and support keyboard operation. The interface
MUST communicate actionable failure states when an operation can fail, without exposing
implementation details. The MVP MUST remain simple and functional; visual polish MUST NOT delay
its core subscription-management behavior.

## Technical Constraints

The application uses an ASP.NET Core Web API backend and a Blazor WebAssembly frontend and MUST
support local development on Windows, macOS, and Linux. The MVP stores subscriptions in memory and
does not fetch or parse feeds. Feed parsing, if approved for the Extended-MVP, SHOULD use
`System.ServiceModel.Syndication` for basic RSS/Atom support. Backend and frontend origins and API
base URLs MUST be configured consistently for local development; cross-origin access MUST be
limited to the required frontend origins.

## Development Workflow and Quality Gates

Each change MUST map to an approved project requirement or explicitly approved scope expansion.
Before review, contributors MUST run the relevant build and automated tests and verify affected
user flows. API changes MUST include tests for the changed contract. Reviews MUST check scope,
security, maintainability, accessibility where UI changes are involved, and compliance with this
constitution. Known limitations and failing checks MUST be disclosed; they MUST NOT be represented
as successful validation.

## Governance

This constitution governs project implementation and takes precedence over conflicting local
conventions. Amendments MUST be made through a reviewed change that explains the rationale and
impact on existing requirements or implementation. Contributors and reviewers MUST evaluate
changes against these principles and record any approved exception with its rationale and scope.

The constitution uses semantic versioning. A MAJOR increment is required for incompatible
principle changes or removals; a MINOR increment is required for a new principle, section, or
materially expanded requirement; a PATCH increment is for clarifications and non-semantic
wording changes. The ratification date records the original adoption date and MUST NOT change
when later amendments are made.

**Version**: 1.0.0 | **Ratified**: TODO(RATIFICATION_DATE): confirm original adoption date | **Last Amended**: 2026-10-25
