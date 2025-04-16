using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using TaskFlow.Core.Exceptions;
using TaskFlow.Exceptions;
using TaskFlow.Helpers;

namespace TaskFlow.Middlewares;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BadRequestException ex)
        {
            await HandleExceptionAsync(context, ex.StatusCode, ex.Message);
        }
        catch (NotFoundException ex)
        {
            await HandleExceptionAsync(context, ex.StatusCode, ex.Message);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, StatusCodes.Status500InternalServerError,
                "An unhandled exception occurred.");
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, int statusCode, string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var errorResponse = ApiResponse<string>.ErrorResponse(null, message);
        var json = JsonConvert.SerializeObject(errorResponse);
        await context.Response.WriteAsync(json);
    }
}