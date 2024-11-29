using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TaskFlow.Helpers;

namespace TaskFlow.Middlewares;

public class ApiResponseMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ApiResponseMiddleware> _logger;

    public ApiResponseMiddleware(RequestDelegate next, ILogger<ApiResponseMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        var originalBodyStream = context.Response.Body;
        var newBodyStream = new MemoryStream(); // Remove 'using' and manage disposal manually
        context.Response.Body = newBodyStream;

        try
        {
            await _next(context);

            var contentType = context.Response.ContentType;
            if (contentType?.Contains("application/json") == true)
            {
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                var bodyData = await new StreamReader(newBodyStream).ReadToEndAsync();

                object responseObject = JsonConvert.DeserializeObject<object>(bodyData);

                string message = context.Request.Path.Value?.Split('/').Last() switch
                {
                    "register" => "User Created Successfully",
                    "login" => "Login Successful",
                    "GetUserByName" => "User Retrieved Successfully",
                    _ => "Request Successful"
                };

                var standardResponse = ApiResponse<object>.SuccessResponse(responseObject, message);
                var standardResponseJson = JsonConvert.SerializeObject(standardResponse);

                context.Response.ContentType = "application/json";
                context.Response.ContentLength = Encoding.UTF8.GetByteCount(standardResponseJson);
                context.Response.Body = originalBodyStream;
                await context.Response.WriteAsync(standardResponseJson);
            }
            else
            {
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                await newBodyStream.CopyToAsync(originalBodyStream);
            }
        }
        // catch (CustomValidationException ex)
        // {
        //     _logger.LogError($"Validation failed: {JsonConvert.SerializeObject(ex.Errors)}");

        //     var errorResponse = ApiResponse<object>.ErrorResponse(ex.Errors, "Validation Failed");
        //     var errorResponseJson = JsonConvert.SerializeObject(errorResponse);

        //     context.Response.StatusCode = 400;
        //     context.Response.ContentType = "application/json";
        //     await context.Response.WriteAsync(errorResponseJson);
        // }
        catch (Exception ex)
        {
            var standardResponse = ApiResponse<object>.ErrorResponse(null, ex.Message);
            var standardResponseJson = JsonConvert.SerializeObject(standardResponse);
            context.Response.ContentType = "application/json";
            context.Response.ContentLength = Encoding.UTF8.GetByteCount(standardResponseJson);
            context.Response.Body = originalBodyStream;
            await context.Response.WriteAsync(standardResponseJson);
        }
        finally
        {
            context.Response.Body = originalBodyStream;
            newBodyStream.Dispose(); // Dispose manually here
        }
    }

}