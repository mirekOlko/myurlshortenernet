using URLShortner.Api.Core.Tests;
using URLShortner.Api.Tests.TestDoubles;
using URLShortner.Core;
using URLShortner.Core.Urls.Add;

namespace URLShortner.Api.Tests;

public class AddUrlScenarios
{
    private readonly AddUrlHandler _addUrlHandler;
    private readonly InMemoryUrlDataStore _urlDataStore;

    public AddUrlScenarios()
    {
        var tokenProvider = new TokenProvider();
        tokenProvider.AssignRange(1,5);
        var shortUrlGenerator = new ShortUrlGenerator(tokenProvider);
        _urlDataStore = new InMemoryUrlDataStore();
        _addUrlHandler = new AddUrlHandler(shortUrlGenerator, _urlDataStore);
    }
    
    [Fact]
    public async Task Should_return_shortened_url()
    {
        // Arrange
        var request = CreateAddUrlRequest();
        // Act
        var response = await _addUrlHandler.HandleAsync(request, default);
        // Assert
        response.ShortUrl.Should().NotBeEmpty();
        response.ShortUrl.Should().Be("1");
    }

    [Fact]
    public async Task Should_save_short_url()
    {
        // Arrange
        var request = CreateAddUrlRequest();
        // Act
        var response = await _addUrlHandler.HandleAsync(request, default);
        // Assert
        _urlDataStore.Should().ContainKey(response.ShortUrl);
    }

    private static AddUrlRequest CreateAddUrlRequest()
    {
        return new AddUrlRequest(new Uri("http://url.com"));
    }
}

