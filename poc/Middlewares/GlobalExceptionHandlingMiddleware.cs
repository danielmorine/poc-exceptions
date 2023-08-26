using Microsoft.AspNetCore.Mvc;
using poc.CustomExceptions;
using poc.Helpers;
using System.Text.Json;

namespace poc.Middlewares
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }           
            catch (Exception ex)
            {
                _logger.LogError(ex,
                               message: ex.Message);

                var exceptionType = ex.GetType().Name;

                var httpStatusCode = exceptionType.Equals(nameof(CustomException)) ? 400 :
                                     exceptionType.Equals(nameof(CustomTimeoutException)) ? 504 :
                                     500;

                context.Response.StatusCode = httpStatusCode;

                ProblemDetails problem = new()
                {
                    Status = httpStatusCode,
                    Type = httpStatusCode.GetTypeMessage(),
                    Title = httpStatusCode.GetTitleMessage(),
                    Detail = httpStatusCode.GetDetailMessage(ex.Message)
                };

                string json = JsonSerializer.Serialize(problem);

                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(json);
                await context.Response.Body.FlushAsync();
            }
        }        
    }
}
