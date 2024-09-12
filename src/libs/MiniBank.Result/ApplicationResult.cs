using System.Runtime.InteropServices;

namespace MiniBank.Result;


public class ApplicationResult
{

    public bool IsError
    {
        get => !IsSucess;
    }

    public object? Payload
    {
        get;
        private set;
    }

    public string Message
    {
        get;
        protected set;
    }

    public bool IsSucess
    {
        get;
        protected set;
    }

    public static ApplicationResult IsSuccess<TPayload>(string message, TPayload payload) where TPayload : class
    {
        return CreateInternal(message, true, payload);
    }

    public static ApplicationResult IsError(string message)
    {
        return CreateInternal(message);
    }

    private static ApplicationResult CreateInternal<TPayload>(string message, bool isSuccess = false, TPayload? payload = default)
    {
        return new ApplicationResult()
        {
            Message = message,
            IsSucess = true,
            Payload = payload
        };
    }

}


