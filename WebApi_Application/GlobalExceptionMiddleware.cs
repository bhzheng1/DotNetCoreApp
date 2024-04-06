using System.Diagnostics;

namespace WebApi_Application
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<GlobalExceptionMiddleware> logger;
        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "Request error: {Machine}, TraceId: {TraceId}",
                    Environment.MachineName, Activity.Current?.Id);

                await Results.Problem(
                    title: "Error happened!",
                    statusCode: StatusCodes.Status500InternalServerError,
                    extensions: new Dictionary<string, object?> { { "TraceId", Activity.Current?.Id } }
                    ).ExecuteAsync(httpContext);
            }
        }
    }
}

