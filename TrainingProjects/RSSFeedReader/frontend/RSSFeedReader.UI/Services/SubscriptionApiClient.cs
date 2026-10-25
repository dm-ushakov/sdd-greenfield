using System.Net.Http.Json;
using RSSFeedReader.UI.Models;

namespace RSSFeedReader.UI.Services;

public sealed class SubscriptionApiClient(HttpClient httpClient)
{
    public async Task<IReadOnlyList<FeedSubscription>> GetSubscriptionsAsync(
        CancellationToken cancellationToken = default)
    {
        var subscriptions = await httpClient.GetFromJsonAsync<FeedSubscription[]>(
            "subscriptions",
            cancellationToken);

        return subscriptions
            ?? throw new InvalidOperationException("The subscriptions API returned an empty response.");
    }

    public async Task<FeedSubscription> AddSubscriptionAsync(
        string url,
        CancellationToken cancellationToken = default)
    {
        using var response = await httpClient.PostAsJsonAsync(
            "subscriptions",
            new AddSubscriptionRequest(url),
            cancellationToken);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<FeedSubscription>(
                cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("The subscriptions API returned an empty response.");
    }
}
