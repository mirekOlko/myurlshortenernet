﻿using System.Net;
using System.Net.Http.Json;
using URLShortner.Core.Urls.Add;

namespace URLShortener.Tets;

public class AddUrlFeature: IClassFixture<ApiFixture>
{
    private readonly HttpClient _client;

    public AddUrlFeature(ApiFixture fixture)
    {
        _client = fixture.CreateClient();
    }
    
    [Fact]
    public async Task Given_long_url_Should_return_short_url()
    {
        var response = await _client.PostAsJsonAsync<AddUrlRequest>("/api/urls",
            new AddUrlRequest(new Uri("https://foo.bar.com"), "admin") 
            );
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var addUrlResponse = await response.Content.ReadFromJsonAsync<AddUrlResponse>();
        addUrlResponse?.ShortUrl.Should().NotBeNull();
    }
}