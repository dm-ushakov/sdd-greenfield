namespace RSSFeedReader.UI.Models;

public sealed record FeedSubscription(string Url);

public sealed record AddSubscriptionRequest(string Url);
