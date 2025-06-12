using URLShortner.Api.Core.Tests;

namespace URLShortner.Core.Urls.Add;

public interface IUrlDataStore
{
    Task AddAsync(ShortenedUrl shortened, CancellationToken cancellationToken);
}

public class AddUrlHandler
{
    private readonly ShortUrlGenerator _shortUrlGenerator;
    private readonly IUrlDataStore _urlDataStore;

    public AddUrlHandler(ShortUrlGenerator shortUrlGenerator, IUrlDataStore urlDataStore)
    {
        _shortUrlGenerator = shortUrlGenerator ?? throw new ArgumentNullException(nameof(shortUrlGenerator));;
        _urlDataStore = urlDataStore ?? throw new ArgumentNullException(nameof(urlDataStore));
    }

    public async Task<AddUrlResponse> HandleAsync(AddUrlRequest request, CancellationToken cancellationToken)
    {
        
        var shortenedUrl = new ShortenedUrl()
        {
            LongUrl = request.LongUrl,
            ShortUrl = _shortUrlGenerator.GenerateUniqueUrl()
        };
        
        await _urlDataStore.AddAsync(shortenedUrl, cancellationToken); 
        return new AddUrlResponse(shortenedUrl.LongUrl, shortenedUrl.ShortUrl);
    }
}