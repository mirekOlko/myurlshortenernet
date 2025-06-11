namespace URLShortner.Core;

public class TokerProvider
{
    private TokenRange? _tokenRange; 

    public void AssignRange(long start, long end)
    {
        _tokenRange = new TokenRange(start, end);
    }
    
    public void AssignRange(TokenRange tokenRange)
    {
       _tokenRange = tokenRange;
    }

    public long GetToken()
    {
        return _tokenRange.Start;
    }
}