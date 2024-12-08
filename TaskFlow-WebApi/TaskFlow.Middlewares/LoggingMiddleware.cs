using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace TaskFlow.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var originalBodyStream = context.Response.Body;
            var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            var logBuilder = new StringBuilder();

            try
            {
                logBuilder.Append($"[Method: {context.Request.Method}] | [Path: {context.Request.Path}]");
                var userIdentity = context.User?.Identity?.Name ?? "Anonymous";
                logBuilder.Append($" | [User: {userIdentity}]");

                await _next(context);

                stopwatch.Stop();

                logBuilder.Append($" | [Response Time: {stopwatch.ElapsedMilliseconds}ms]");
                logBuilder.Append($" | [Status Code: {context.Response.StatusCode}]");

                context.Response.Body.Seek(0, SeekOrigin.Begin);
                var responseContent = await new StreamReader(context.Response.Body).ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);

#if DEBUG
                logBuilder.Append($" | [Response: {responseContent}]");
#endif

            }
            catch (Exception ex)
            {
                logBuilder.Append($" | [Exception: {ex.Message}]");
                _logger.LogError(logBuilder.ToString());
                throw;
            }
            finally
            {
                context.Response.Body = originalBodyStream;
                await responseBody.CopyToAsync(originalBodyStream);
                responseBody.Dispose();
            }

            _logger.LogInformation(logBuilder.ToString());
        }
    }
}
