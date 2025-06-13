namespace URLShortner.Core;

public class Result<TValue>
{
    private readonly TValue? _value;
    private readonly Error _error;
    private readonly bool _isSucceeded;

    private Result(TValue value)
    {
        _isSucceeded = true;
        _value = value;
        _error = Error.None;
    }
    
    private Result(Error error)
    {
        _isSucceeded = false;
        _value = default;
        _error = error;
    }
    
    public bool Succeeded => _isSucceeded;
    public Error Error => _error;
    public TValue? Value => _value;

    public static Result<TValue> Success(TValue value) => new(value);
    public static Result<TValue> Failure(Error error) => new(error);
    
    public static implicit operator Result<TValue>(TValue value) => new(value);
    public static implicit operator Result<TValue>(Error error) => new(error);

    public TResult Match<TResult>(
        Func<TValue, TResult> success,
        Func<Error, TResult> failure
    )
    {
        return _isSucceeded ? success(_value!) : failure(_error);
    }
}