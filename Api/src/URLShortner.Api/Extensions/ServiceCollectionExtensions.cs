using URLShortner.Api.Core.Tests;
using URLShortner.Core;
using URLShortner.Core.Urls;
using URLShortner.Core.Urls.Add;

namespace URLShortner.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUrlFeature(this IServiceCollection services)
    {
        services.AddScoped<AddUrlHandler>();
        services.AddSingleton<TokenProvider>(_ =>
        {
            var tokenPrivider = new TokenProvider();
            tokenPrivider.AssignRange(1, 1000);
            return tokenPrivider;
        });
        services.AddScoped<ShortUrlGenerator>();
        services.AddSingleton<IUrlDataStore, InMemoryUrlDataStore>();

        return services;
    }
}

public class InMemoryUrlDataStore : Dictionary<string,ShortenedUrl>, IUrlDataStore
{
    public Task AddAsync(ShortenedUrl shortened, CancellationToken cancellationToken)
    {
        Add(shortened.ShortUrl, shortened);
        return Task.CompletedTask;
    }
}