using URLShortner.Core;
using Xunit;

namespace URLShortner.Api.Core.Tests;

public class ShortUrlGeneratorScenario
{
   
    [Fact]
    public void Should_return_short_url_for_zero()
    {
        var tokerProvider = new TokerProvider();
        tokerProvider.AssignRange(0, 10);
        var shortUrlGenerator = new ShortUrlGenerator(tokerProvider);
        var shortUrl = shortUrlGenerator.GenerateUniqueUrl();
        shortUrl.Should().Be("0");
    }

    [Fact]
    public void Should_return_short_url_for_10001()
    {
        var tokerProvider = new TokerProvider();
        tokerProvider.AssignRange(10001, 20000);
        var shortUrlGenerator = new ShortUrlGenerator(tokerProvider);
        var shortUrl = shortUrlGenerator.GenerateUniqueUrl();
        shortUrl.Should().Be("2bJ");
    }
}