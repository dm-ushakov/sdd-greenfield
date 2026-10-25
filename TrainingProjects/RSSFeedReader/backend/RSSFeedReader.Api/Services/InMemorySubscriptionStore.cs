using RSSFeedReader.Api.Models;

namespace RSSFeedReader.Api.Services;

public sealed class InMemorySubscriptionStore
{
    private readonly Lock _lock = new();
    private readonly List<FeedSubscription> _subscriptions = [];

    public FeedSubscription Add(string url)
    {
        var subscription = new FeedSubscription(url);

        lock (_lock)
        {
            _subscriptions.Add(subscription);
        }

        return subscription;
    }

    public IReadOnlyList<FeedSubscription> GetAll()
    {
        lock (_lock)
        {
            return _subscriptions.ToArray();
        }
    }
}
