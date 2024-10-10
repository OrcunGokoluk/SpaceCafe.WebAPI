using MediatR;
using SpaceCafe.Application.Authentication.Common;

namespace SpaceCafe.Application.Authentication.Commands.Register;
public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : IRequest<AuthenticationResult>;
