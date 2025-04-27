namespace MiniBank.ResultPattern
{

    public class ResultBase
    {
        Error _error;
        string _message;

        public bool IsSuccess => _error != null;
        public bool IsError => _error == null;
        public string Message => _message;


        public ResultBase()
        {
        }

        public ResultBase(Error error)
        {
            Error = error;
        }

        public Error Error
        {
            get => _error;
            protected set => _error = value;
        }

    }

    public class Result : ResultBase
    {
        public object Payload { get; set; }

        public Result()
        {
        }

        public Result(Error error) : base(error)
        {

        }

        public static Result<T> Success<T>(T payload)
        {
            return new Result<T>
            {
                Payload = payload,
            };
        }

        public static Result<Error> Failure(string message)
        {
            var result = new Result<Error>(new Error(message));
            return result;
        }


        public static Result<Error> Failure(int code, string message)
        {
            var result = new Result<Error>(new Error(code, message));

            return result;
        }


    }

    public class Result<T> : ResultBase
    {
        public T Payload { get; set; }

        public Result()
        {
        }

        public Result(Error error) : base(error)
        {
        }


        public static implicit operator Result<T>(Result<Error> sourceResult)
        {
            return sourceResult;
        }

    }

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

}