
using Microsoft.AspNetCore.Http;

namespace SpaceCafe.Application.Common.CustomExceptions;
public class CustomException : Exception
{
    public int StatusCode { get; }
    public string Title { get; }
    public string CustomMessage { get; }

    public CustomException(string message) : base(message)
    {
        StatusCode = StatusCodes.Status400BadRequest;
        Title = "An Error Occur";
        CustomMessage = message;
    }
}