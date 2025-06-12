using URLShortner.Core.Urls;
using URLShortner.Core.Urls.Add;

namespace URLShortner.Api.Tests.TestDoubles;

public class InMemoryUrlDataStore : Dictionary<string,ShortenedUrl>, IUrlDataStore
{
    public Task AddAsync(ShortenedUrl shortened, CancellationToken cancellationToken)
    {
        Add(shortened.ShortUrl, shortened);
        return Task.CompletedTask;
    }
}