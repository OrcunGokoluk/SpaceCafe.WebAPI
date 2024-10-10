namespace SpaceCafe.Application.Common.CustomExceptions;
public class CustomValidationException : Exception
{
    public IDictionary<string, string> Errors { get; }

    public CustomValidationException(IDictionary<string, string> errors)
    {
        Errors = errors;
    }
}
