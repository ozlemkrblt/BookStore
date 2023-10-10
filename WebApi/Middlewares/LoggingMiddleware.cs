using System.Diagnostics;
using System.Text;
using WebApi.Services;

namespace WebApi.Middlewares;

public class LoggingMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILoggerService logger;

    public LoggingMiddleware(RequestDelegate next, ILoggerService logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var watch = Stopwatch.StartNew();
        String message = " [Request] HTTP " + context.Request.Method
               + " - " + context.Request.Path;
        logger.Log(message);
        await next(context);    


        watch.Stop();
        message = $"[Response] HTTP {context.Request.Method} - {context.Request.Path} responded HTTP Status Code {context.Response.StatusCode} in {watch.Elapsed.TotalMilliseconds} ms.";
        logger.Log(message);


    }


}


public static class LoggingMiddlewareExtension
{
    public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LoggingMiddleware>();
    }
}