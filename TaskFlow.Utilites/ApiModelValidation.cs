using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using TaskFlow.Helpers;

namespace TaskFlow.Utilites;

public static class ApiModelValidation
{
    public static IServiceCollection AddValidationForModel(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var errors = context.ModelState
                                       .Where(x => x.Value.Errors.Any())
                                       .ToDictionary(
                                           x => x.Key,
                                           x => x.Value.Errors.Select(e => e.ErrorMessage).ToList()
                                       );
                context.HttpContext.Response?.Headers?.Add("X-Skip-Middleware", "true");
                var errorResponse = ApiResponse<object>.ErrorResponse(errors, "Validation Failed");

                return new BadRequestObjectResult(errorResponse);
            };
        });
        return services;

    }

}
