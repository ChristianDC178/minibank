using Microsoft.AspNetCore.Builder;

namespace MiniBank.Exceptions;

public static class MinibankExceptionHanlderMiddlewareExtensions
{
    public static IApplicationBuilder UseMinibankCustomExceptionHandler(this IApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        return builder.UseMiddleware<MinibankExceptionHandlerMiddleware>();
    }
}
