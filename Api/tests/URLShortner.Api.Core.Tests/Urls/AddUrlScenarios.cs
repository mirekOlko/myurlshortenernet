using Microsoft.Extensions.Time.Testing;
using URLShortner.Api.Core.Tests;
using URLShortner.Api.Tests.TestDoubles;
using URLShortner.Core;
using URLShortner.Core.Urls.Add;

namespace URLShortner.Api.Tests;

public class AddUrlScenarios
{
    private readonly AddUrlHandler _addUrlHandler;
    private readonly InMemoryUrlDataStore _urlDataStore;
    private readonly FakeTimeProvider _timeProvider;

    public AddUrlScenarios()
    {
        var tokenProvider = new TokenProvider();
        tokenProvider.AssignRange(1,5);
        var shortUrlGenerator = new ShortUrlGenerator(tokenProvider);
        _urlDataStore = new InMemoryUrlDataStore();
        _timeProvider = new FakeTimeProvider();
        _addUrlHandler = new AddUrlHandler(shortUrlGenerator, _urlDataStore, _timeProvider);
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
    
    [Fact]
    public async Task Should_save_short_url_with_created_by_and_created_on()
    {
        // Arrange
        var request = CreateAddUrlRequest();
        // Act
        var response = await _addUrlHandler.HandleAsync(request, default);
        // Assert
        _urlDataStore.Should().ContainKey(response.ShortUrl);
        _urlDataStore[response.ShortUrl].CreatedBy.Should().Be(request.CreatedBy);
        _urlDataStore[response.ShortUrl].CreatedOn.Should().Be(_timeProvider.GetUtcNow());

    }

    private static AddUrlRequest CreateAddUrlRequest()
    {
        return new AddUrlRequest(new Uri("http://url.com"),
            "foo@foobar.com");
    }
}

