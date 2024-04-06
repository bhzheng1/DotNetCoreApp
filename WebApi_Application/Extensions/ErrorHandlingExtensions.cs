using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;

namespace WebApi_Application.Extensions
{
    public static class ErrorHandlingExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
                var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

                logger.LogError(
                exception,
                "Request error: {Machine}, TraceId: {TraceId}",
                Environment.MachineName, Activity.Current?.Id);

                //await Results.Problem(
                //    title: "Error happened!",
                //    statusCode: StatusCodes.Status500InternalServerError,
                //    extensions: new Dictionary<string, object?> { { "TraceId", Activity.Current?.Id } }
                //    ).ExecuteAsync(context);

                var response = new { error = exception?.Message, exception = exception?.GetType().Name };
                await context.Response.WriteAsJsonAsync(response);
            });
        }
    }
}

