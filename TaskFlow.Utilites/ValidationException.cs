public class CustomValidationException : Exception
{
    public List<string> Errors { get; set; }

    public CustomValidationException(List<string> errors, string message = "Validation failed") : base(message)
    {
        Errors = errors;
    }
}
