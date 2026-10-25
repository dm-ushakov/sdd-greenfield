using RSSFeedReader.Api.Models;
using RSSFeedReader.Api.Services;

namespace RSSFeedReader.Api.Endpoints;

public static class SubscriptionEndpoints
{
    public static IEndpointRouteBuilder MapSubscriptionEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/subscriptions", (InMemorySubscriptionStore store) =>
            Results.Ok(store.GetAll()));

        endpoints.MapPost("/api/subscriptions", AddSubscription);

        return endpoints;
    }

    private static IResult AddSubscription(AddSubscriptionRequest? request, InMemorySubscriptionStore store)
    {
        if (string.IsNullOrWhiteSpace(request?.Url))
        {
            return Results.BadRequest(new { message = "A non-empty URL is required." });
        }

        return Results.Ok(store.Add(request.Url));
    }
}
