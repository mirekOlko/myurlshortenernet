namespace URLShortner.Core.Urls;

public class ShortenedUrl
{
    public ShortenedUrl(Uri longUrl, string shortUrl, string createdBy, DateTimeOffset createdOn)
    {
        LongUrl = longUrl;
        ShortUrl = shortUrl;
        CreatedBy = createdBy;
        CreatedOn = createdOn;
    }
    
    public Uri LongUrl { get; set; }
    public string ShortUrl { get; set; }
    public string CreatedBy { get; }
    public DateTimeOffset CreatedOn { get; }
    
}