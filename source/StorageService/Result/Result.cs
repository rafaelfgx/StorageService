namespace StorageService;

public record Result(ResultType Type, string Message)
{
    public bool HasMessage => !string.IsNullOrWhiteSpace(Message);

    public bool IsError => Type == ResultType.Error;

    public bool IsSuccess => Type == ResultType.Success;

    public static Result Error(string message) => new(ResultType.Error, message);

    public static Result Success() => new(ResultType.Success, default);
}

public sealed record Result<T>(ResultType Type, string Message, T Value) : Result(Type, Message)
{
    public bool HasValue => Value is not null && !Equals(Value, default(T));

    public new static Result<T> Error(string message) => new(ResultType.Error, message, default);

    public static Result<T> Success(T value) => new(ResultType.Success, default, value);
}

public enum ResultType
{
    None = 0,
    Success = 1,
    Error = 2
}
