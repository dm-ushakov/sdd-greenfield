# Subscription API Contract

The API is local to the application. The base URL is configured in the frontend and must match
the backend's configured origin. The documented local API origin is `http://localhost:5151`.

## `GET /api/subscriptions`

Returns the subscriptions added since the API process started, in submission order.

**Success response**: `200 OK`

```json
[
  { "url": "https://example.com/feed.xml" }
]
```

When no subscriptions have been added, return `200 OK` with an empty array.

## `POST /api/subscriptions`

Adds one subscription to the current in-memory list. The request body is:

```json
{ "url": "https://example.com/feed.xml" }
```

**Success response**: `200 OK`

```json
{ "url": "https://example.com/feed.xml" }
```

**Invalid request**: `400 Bad Request` when `url` is missing, empty, or whitespace-only. The
invalid value is not added. The response must provide a client-safe validation message.

The API does not fetch the URL, verify that it is reachable, or parse feed content. Duplicate
submissions are accepted as separate list entries.
