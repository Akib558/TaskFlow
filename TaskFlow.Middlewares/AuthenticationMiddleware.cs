using System;
using Microsoft.AspNetCore.Http;

namespace TaskFlow.Middlewares;

public class AuthenticationMiddleware
{
    public readonly RequestDelegate _next;

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"];
        if (token.Count == 0)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Token is missing");
            return;
        }
        //TODO: Implement JWT token validation
        /*
            - during login check if the requested middleware is in the list of allowed middlewares
            - or for any request check if the token is valid
        */

        await _next(context);
    }
}
