using MediatR;
using Microsoft.AspNetCore.Mvc;
using OneOf;
using SpaceCafe.Application.Authentication.Commands.Register;
using SpaceCafe.Application.Authentication.Common;
using SpaceCafe.Application.Authentication.Queries.Login;
using SpaceCafe.Application.Common.CustomException;
using SpaceCafe.Contracts.Authentication;

namespace SpaceCafe.WebAPI.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController(ISender _mediator) : Controller
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {

        var command = new RegisterCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        OneOf<AuthenticationResult, DuplicateEmailError, CustomException> authResult = await _mediator.Send(command);


        return authResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(statusCode: StatusCodes.Status409Conflict, title: "I dunno"),
             customException => Problem(title: customException.Title, detail: customException.CustomMessage, statusCode: customException.StatusCode)
            );

    }



    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var command = new LoginQuery(
            request.Email,
            request.Password);

        OneOf<AuthenticationResult, DuplicateEmailError, CustomException> authResult = await _mediator.Send(command);

        return authResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(statusCode: StatusCodes.Status409Conflict, title: "I dunno"),
            customException => Problem(title: customException.Title, detail: customException.CustomMessage, statusCode: customException.StatusCode)
            );

    }

    private static AuthenticationResponse MapAuthResult(AuthenticationResult authservice)
    {
        var response = new AuthenticationResponse
                        (
                            authservice.user.Id,
                            authservice.user.FirstName,
                            authservice.user.LastName,
                            authservice.user.Email,
                            //autservice.Id,
                            //autservice.FirstName,
                            //autservice.LastName,
                            //autservice.Email,
                            authservice.Token);
        return response;
    }


}
