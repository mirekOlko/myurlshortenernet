namespace URLShortner.Core;

public record TokenRange
{
    public TokenRange(long start, long end)
    {
        if (end < start)
            throw new ArgumentException("End must be greater than Start");
        Start = start;
        End = end;
    }

    public long Start { get; }
    public long End { get; }
};