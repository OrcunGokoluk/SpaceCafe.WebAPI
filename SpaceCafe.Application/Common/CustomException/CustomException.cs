
using Microsoft.AspNetCore.Http;

namespace SpaceCafe.Application.Common.CustomException;
public class CustomException : Exception
{
    public int StatusCode { get; }
    public string Title { get; }

    public string Message { get; }

    public CustomException(string message) : base(message)
    {
        StatusCode = StatusCodes.Status400BadRequest;
        Title = "An Error Occur";
        Message = message;

    }
}