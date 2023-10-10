using System.Diagnostics;
using System.Net;
using Newtonsoft.Json;
using WebApi.Services;

namespace WebApi.Middlewares
{
    public class CustomExceptionMiddleware
    {

        private readonly RequestDelegate next;

        ILoggerService logger;
        public CustomExceptionMiddleware(RequestDelegate next, ILoggerService logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {

            var watch = Stopwatch.StartNew();
            try
            {


                String message = " [Request] HTTP " + context.Request.Method
                    + " - " + context.Request.Path;
                logger.Log(message);
                await next(context);

                watch.Stop();

                message = " [Response] HTTP " + context.Request.Method
                    + " - " + context.Request.Path
                    + " responded HTTP Status Code " + context.Response.StatusCode
                    + " in " + watch.Elapsed.TotalMilliseconds + " ms. ";
                logger.Log(message);
            }
            catch (Exception ex)
            {
                watch.Stop();
                await HandleException(context, ex, watch);
            }
        }

        private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = " [Error] HTTP " + context.Request.Method + " - " + context.Response.StatusCode
                + " Error Message " + ex.Message + " in " + watch.ElapsedMilliseconds + " ms. ";
            logger.Log(message);


            var result = JsonConvert.SerializeObject(new { error = ex.Message }, Formatting.None);

            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
