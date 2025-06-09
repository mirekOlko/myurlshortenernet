using URLShortner.Core;
using Xunit;
namespace URLShortner.Api.Core.Tests;

public class Base62EncodingScenarios
{

    [Theory]
    [InlineData(0, "0")]
    [InlineData(1, "1")]
    [InlineData(20, "k")]
    [InlineData(1000, "g8")]
    [InlineData(61, "Z")]
    [InlineData(987654321, "14Q60p")]
    public void Should_encode_number_to_base62(int number, string expected)
    {
        number.EncodeToBase62().Should().Be(expected);
    }
}
