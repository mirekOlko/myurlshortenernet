using URLShortner.Api.Core.Tests;

namespace URLShortner.Core.Urls.Add;

public class AddUrlHandler
{
    private readonly ShortUrlGenerator _shortUrlGenerator;

    public AddUrlHandler(ShortUrlGenerator shortUrlGenerator)
    {
        _shortUrlGenerator = shortUrlGenerator;
    }

    public Task<AddUrlResponse> HandleAsync(AddUrlRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new AddUrlResponse(request.LongUrl, _shortUrlGenerator.GenerateUniqueUrl()));
    }
}