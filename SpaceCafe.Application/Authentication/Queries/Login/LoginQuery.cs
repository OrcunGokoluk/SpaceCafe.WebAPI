using MediatR;
using SpaceCafe.Application.Authentication.Common;

namespace SpaceCafe.Application.Authentication.Queries.Login;
public record LoginQuery(
    string Email,
    string Password) : IRequest<AuthenticationResult>;
