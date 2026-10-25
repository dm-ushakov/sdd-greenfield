using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using RSSFeedReader.Api.Models;

namespace RSSFeedReader.Api.Tests;

public sealed class SubscriptionsApiTests
{
    [Fact]
    public async Task GetSubscriptions_WhenEmpty_ReturnsEmptyList()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var subscriptions = await client.GetFromJsonAsync<FeedSubscription[]>("/api/subscriptions");

        Assert.NotNull(subscriptions);
        Assert.Empty(subscriptions);
    }

    [Fact]
    public async Task PostSubscription_ThenGet_ReturnsSubscriptionsInOrder()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var first = await client.PostAsJsonAsync(
            "/api/subscriptions",
            new { url = "https://example.com/first.xml" });
        var second = await client.PostAsJsonAsync(
            "/api/subscriptions",
            new { url = "https://example.com/second.xml" });
        var subscriptions = await client.GetFromJsonAsync<FeedSubscription[]>("/api/subscriptions");

        Assert.Equal(HttpStatusCode.OK, first.StatusCode);
        Assert.Equal(HttpStatusCode.OK, second.StatusCode);
        Assert.NotNull(subscriptions);
        Assert.Equal(
            ["https://example.com/first.xml", "https://example.com/second.xml"],
            subscriptions.Select(subscription => subscription.Url));
    }

    [Theory]
    [InlineData("{}")]
    [InlineData("{\"url\":\"\"}")]
    [InlineData("{\"url\":\"   \"}")]
    public async Task PostSubscription_WhenUrlIsMissingOrBlank_ReturnsBadRequestWithoutAdding(string body)
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        using var content = new StringContent(body, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/api/subscriptions", content);
        var subscriptions = await client.GetFromJsonAsync<FeedSubscription[]>("/api/subscriptions");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.NotNull(subscriptions);
        Assert.Empty(subscriptions);
    }
}
