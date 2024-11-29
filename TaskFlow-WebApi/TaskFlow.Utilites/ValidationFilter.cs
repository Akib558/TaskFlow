using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging; // Add this if you plan to log

namespace TaskFlow.Utilities
{
    public class ValidationFilter : IActionFilter
    {
        private readonly ILogger<ValidationFilter> _logger;  // Add logger for logging errors

        // Constructor to inject logger
        public ValidationFilter(ILogger<ValidationFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

            if (!context.ModelState.IsValid)
            {
                // Collect all error messages
                var errors = context.ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToList();

                // Log the validation errors
                _logger.LogError("Validation failed: {Errors}", string.Join(", ", errors));
                return;
                // Throw custom validation exception
                // throw new CustomValidationException(errors);
            }

        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
