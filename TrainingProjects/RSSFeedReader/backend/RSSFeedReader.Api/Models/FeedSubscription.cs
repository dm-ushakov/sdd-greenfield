namespace RSSFeedReader.Api.Models;

public sealed record FeedSubscription
{
    public FeedSubscription(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new ArgumentException("A non-empty URL is required.", nameof(url));
        }

        Url = url;
    }

    public string Url { get; }
}
