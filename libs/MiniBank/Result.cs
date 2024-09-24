namespace MiniBank;

public class Error
{
    public int? Code { get; private set; }
    public string Message { get; private set; }

    public Error(string message)
    {
        Message = message;
    }

    public Error(int code, string message) : this(message)
    {
        Code = code;
    }
}

public class ResultBase
{
    Error _error;
    string _message;

    public bool IsSuccess => _error != null;
    public bool IsError => _error == null;
    public string Message => _message;

    public Error Error
    {
        get => _error;
        protected set => _error = value;
    }
}

public class Result : ResultBase
{

    public object Payload { get; set; }

    public Result(Error error)
    {
        ArgumentNullException.ThrowIfNull(nameof(error));
        Error = error;
    }

    public static Result<T> Success<T>(T payload)
    {
        return new Result<T>
        {
            Payload = payload,
        };
    }

    public static Result Failure(string message)
    {
        var result = new Result(new Error(message));
        return result;
    }

    public static Result Failure(int code, string message)
    {
        var result = new Result(new Error(code, message));
        return result;
    }
}

public class Result<T> : ResultBase
{
    public T Payload { get; set; }

    public static implicit operator Result(Result<T> sourceResult)
    {
        Result targetResult = null;

        if (sourceResult.IsError)
        {
            targetResult = new Result(sourceResult.Error);
        }
        else
        {
            targetResult.Payload = sourceResult.Payload;
        }

        return targetResult;
    }
}
