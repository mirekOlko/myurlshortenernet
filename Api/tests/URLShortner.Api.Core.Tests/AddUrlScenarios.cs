using URLShortner.Api.Core.Tests;
using URLShortner.Core;
using URLShortner.Core.Urls.Add;

namespace URLShortner.Api.Tests;

public class AddUrlScenarios
{
    [Fact]
    public async Task Should_return_shortened_url()
    {
        var tokenProvider = new TokenProvider();
        tokenProvider.AssignRange(1,5);
        var shortUrlGenerator = new ShortUrlGenerator(tokenProvider);
        var handler = new AddUrlHandler(shortUrlGenerator);
        var longUrl = "http://url.com";
        var request = new AddUrlRequest(new Uri(longUrl));
        var response = await handler.HandleAsync(request, default);
        response.ShortUrl.Should().NotBeEmpty();
        response.ShortUrl.Should().Be("1");
    }
}