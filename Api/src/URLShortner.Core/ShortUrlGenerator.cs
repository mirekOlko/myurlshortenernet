using URLShortner.Core;

namespace URLShortner.Api.Core.Tests;

public class ShortUrlGenerator
{
    private readonly TokerProvider _tokenProvider;

    public ShortUrlGenerator(TokerProvider tokerProvider)
    {
        _tokenProvider = tokerProvider;
    }

    public string GenerateUniqueUrl()
    {
        return _tokenProvider
            .GetToken()
            .EncodeToBase62();
    }
}