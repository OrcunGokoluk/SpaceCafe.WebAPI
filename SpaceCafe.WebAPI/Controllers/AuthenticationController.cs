using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SpaceCafe.Application.Authentication.Commands.Register;
using SpaceCafe.Application.Authentication.Queries.Login;
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

        var authResult = await _mediator.Send(command);


        return Ok(_mapper.Map<AuthenticationResponse>(authResult));



    }



    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);

        //var query = new LoginQuery(
        //    request.Email,
        //    request.Password);

        var authResult = await _mediator.Send(query);

        return Ok(_mapper.Map<AuthenticationResponse>(authResult));



    }
}

