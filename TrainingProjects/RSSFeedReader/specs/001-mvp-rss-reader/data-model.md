# Data Model: MVP RSS Reader

## Feed Subscription

Represents one URL submitted by the user and displayed in the current session's subscription
list.

| Field | Type | Required | Rules |
|---|---|---|---|
| URL | String | Yes | Must contain at least one non-whitespace character. No reachability or RSS/Atom format check is performed. |

### Relationships and lifecycle

- A session contains an ordered list of zero or more feed subscriptions.
- Each accepted submission adds one entry; duplicate URLs are not rejected by the MVP.
- Entries remain available while the API process runs and are discarded when it stops.
- No feed-item entity is included because the MVP does not fetch or parse feeds.
