using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace TaskFlow.Middlewares;
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
        _logger.LogInformation($"Incoming Request: {context.Request.Method} {context.Request.Path}");

        var originalBodyStream = context.Response.Body;

        try
        {
            // Use a memory stream to capture the response
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            // Call the next middleware in the pipeline
            await _next(context);

            // Read the response body
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            _logger.LogInformation($"Outgoing Response: {context.Response.StatusCode} {responseText}");

            // Copy the content of the new stream to the original stream
            await responseBody.CopyToAsync(originalBodyStream);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in the LoggingMiddleware.");
            throw; // Re-throw the exception to ensure the pipeline is aware of the error
        }
        finally
        {
            // Restore the original response body stream
            context.Response.Body = originalBodyStream;
        }
    }


}