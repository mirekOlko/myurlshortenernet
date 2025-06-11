using URLShortner.Core;

namespace URLShortner.Api.Tests;

public class TokerRangeScenarios
{
    [Fact]
    public void When_start_tocken_is_greater_than_end_token_then_throws_exception()
    {
        var act = () => new TokenRange(10, 5);
        act.Should().Throw<ArgumentException>().WithMessage("End must be greater than start");
    }
}