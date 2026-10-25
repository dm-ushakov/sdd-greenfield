using RSSFeedReader.Api.Services;

namespace RSSFeedReader.Api.Tests.Services;

public sealed class InMemorySubscriptionStoreTests
{
    [Fact]
    public void Add_PreservesSubmissionOrder()
    {
        var store = new InMemorySubscriptionStore();

        store.Add("https://example.com/first.xml");
        store.Add("https://example.com/second.xml");

        Assert.Equal(
            ["https://example.com/first.xml", "https://example.com/second.xml"],
            store.GetAll().Select(subscription => subscription.Url));
    }

    [Fact]
    public void Add_AcceptsDuplicateUrls()
    {
        var store = new InMemorySubscriptionStore();

        store.Add("https://example.com/feed.xml");
        store.Add("https://example.com/feed.xml");

        Assert.Equal(2, store.GetAll().Count);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Add_RejectsBlankUrls(string url)
    {
        var store = new InMemorySubscriptionStore();

        Assert.Throws<ArgumentException>(() => store.Add(url));
        Assert.Empty(store.GetAll());
    }
}
