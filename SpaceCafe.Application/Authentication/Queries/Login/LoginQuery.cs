using MediatR;
using OneOf;
using SpaceCafe.Application.Authentication.Common;
using SpaceCafe.Application.Common.CustomException;

namespace SpaceCafe.Application.Authentication.Queries.Login;
public record LoginQuery(
    string Email,
    string Password) : IRequest<OneOf<AuthenticationResult, DuplicateEmailError, CustomException>>;
