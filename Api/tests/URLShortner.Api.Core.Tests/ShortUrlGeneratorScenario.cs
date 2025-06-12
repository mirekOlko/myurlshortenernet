using URLShortner.Api.Core.Tests;
using URLShortner.Core;

namespace URLShortner.Api.Tests;

public class ShortUrlGeneratorScenario
{
   
    [Fact]
    public void Should_return_short_url_for_zero()
    {
        // Arrange
        var tokerProvider = new TokenProvider();
        tokerProvider.AssignRange(0, 10);
        var shortUrlGenerator = new ShortUrlGenerator(tokerProvider);
        // Act
        var shortUrl = shortUrlGenerator.GenerateUniqueUrl();
        // Assert
        shortUrl.Should().Be("0");
    }

    [Fact]
    public void Should_return_short_url_for_10001()
    {
        // Arrange
        var tokerProvider = new TokenProvider();
        tokerProvider.AssignRange(10001, 20000);
        var shortUrlGenerator = new ShortUrlGenerator(tokerProvider);
        // Act
        var shortUrl = shortUrlGenerator.GenerateUniqueUrl();
        // Assert
        shortUrl.Should().Be("2bJ");
    }
}