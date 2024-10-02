using MediatR;
using OneOf;
using SpaceCafe.Application.Authentication.Common;
using SpaceCafe.Application.Common.CustomException;

namespace SpaceCafe.Application.Authentication.Commands.Register;
public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : IRequest<OneOf<AuthenticationResult, DuplicateEmailError, CustomException>>;
