using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using TaskFlow.Helpers; // Add this if you plan to log
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Core.Validators;
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace TaskFlow.Utilities
{
    public class ValidationModelFilter : IActionFilter
    {
        private readonly ILogger<ValidationModelFilter> _logger;  // Add logger for logging errors

        // Constructor to inject logger
        public ValidationModelFilter(ILogger<ValidationModelFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(x => x.Value.Errors.Any())
                    .ToDictionary(
                        x => x.Key,
                        x => x.Value.Errors.Select(x => x.ErrorMessage).ToArray()
                    );

                var errorResponse = ApiResponse<object>.ErrorResponse(errors, "Form Validation Failed");
                context.HttpContext.Response?.Headers?.Add("X-Skip-Middleware", "true");

                context.Result = new ObjectResult(errorResponse)
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };

            }

        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
