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
        response.Value!.ShortUrl.Should().NotBeEmpty();
        response.Value!.ShortUrl.Should().Be("1");
    }

    [Fact]
    public async Task Should_save_short_url()
    {
        // Arrange
        var request = CreateAddUrlRequest();
        // Act
        var response = await _addUrlHandler.HandleAsync(request, default);
        // Assert
        response.Succeeded.Should().BeTrue();
        _urlDataStore.Should().ContainKey(response.Value!.ShortUrl);
    }
    
    [Fact]
    public async Task Should_save_short_url_with_created_by_and_created_on()
    {
        // Arrange
        var request = CreateAddUrlRequest();
        // Act
        var response = await _addUrlHandler.HandleAsync(request, default);
        // Assert
        response.Succeeded.Should().BeTrue();
        _urlDataStore.Should().ContainKey(response.Value!.ShortUrl);
        _urlDataStore[response.Value!.ShortUrl].CreatedBy.Should().Be(request.CreatedBy);
        _urlDataStore[response.Value!.ShortUrl].CreatedOn.Should().Be(_timeProvider.GetUtcNow());

    }

    [Fact]
    public async Task Should_return_error_if_created_by_is_empty()
    {
        // Arrange
        var request = CreateAddUrlRequest(String.Empty);
        // Act
        var response = await _addUrlHandler.HandleAsync(request, default);
        // Assert
        response.Succeeded.Should().BeFalse();
        response.Error.Code.Should().Be("missing_value");
    }

    private static AddUrlRequest CreateAddUrlRequest(string createdBy = "foo@foobar.com")
    {
        return new AddUrlRequest(new Uri("http://url.com"),
            createdBy);
    }
}

