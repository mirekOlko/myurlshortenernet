namespace URLShortner.Core.Urls;

public class ShortenedUrl
{
    public ShortenedUrl(Uri longUrl, string shortUrl)
    {
        LongUrl = longUrl;
        ShortUrl = shortUrl;
    }
    
    public Uri LongUrl { get; set; }
    public string ShortUrl { get; set; }
    
}