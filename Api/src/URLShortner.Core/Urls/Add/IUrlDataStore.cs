namespace URLShortner.Core.Urls.Add;

public interface IUrlDataStore
{
    Task AddAsync(ShortenedUrl shortened, CancellationToken cancellationToken);
}