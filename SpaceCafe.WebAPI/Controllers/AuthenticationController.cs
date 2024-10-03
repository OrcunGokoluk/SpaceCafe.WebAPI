using MapsterMapper;
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
public class AuthenticationController(ISender _mediator, IMapper _mapper) : Controller
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        _mapper = new Mapper();

        var command = _mapper.Map<RegisterCommand>(request);

        //artık manuel mapping yerine yukarıdaki mapping'i yaptık

        //var command = new RegisterCommand(
        //    request.FirstName,
        //    request.LastName,
        //    request.Email,
        //    request.Password);

        OneOf<AuthenticationResult, DuplicateEmailError, CustomException> authResult = await _mediator.Send(command);


        return authResult.Match(
            authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
            errors => Problem(statusCode: StatusCodes.Status409Conflict, title: "I dunno"),
             customException => Problem(title: customException.Title, detail: customException.CustomMessage, statusCode: customException.StatusCode)
            );

    }



    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);

        //var query = new LoginQuery(
        //    request.Email,
        //    request.Password);

        OneOf<AuthenticationResult, DuplicateEmailError, CustomException> authResult = await _mediator.Send(query);

        return authResult.Match(
            authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
            errors => Problem(statusCode: StatusCodes.Status409Conflict, title: "I dunno"),
            customException => Problem(title: customException.Title, detail: customException.CustomMessage, statusCode: customException.StatusCode)
            );

    }
}

