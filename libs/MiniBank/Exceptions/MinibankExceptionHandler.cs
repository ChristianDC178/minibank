using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MiniBank.Exceptions
{
    public class MinibankExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MinibankExceptionHandlerMiddleware> _logger;

        public MinibankExceptionHandlerMiddleware(RequestDelegate next, ILogger<MinibankExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "An error occurred");

            // Customize the response here
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // You can customize the error message and structure here
            var result = System.Text.Json.JsonSerializer.Serialize(new { message = "An unexpected error occurred." });

            return response.WriteAsync(result);
        }
    }
}
