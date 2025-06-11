using URLShortner.Core;

namespace URLShortner.Api.Core.Tests;

public class ShortUrlGenerator
{
    private readonly TokenProvider _tokenProvider;

    public ShortUrlGenerator(TokenProvider tokerProvider)
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