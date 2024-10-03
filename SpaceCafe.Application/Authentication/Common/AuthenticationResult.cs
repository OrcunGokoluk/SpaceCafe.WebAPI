using SpaceCafe.Domain.Entities;

namespace SpaceCafe.Application.Authentication.Common;
public record AuthenticationResult(
    User user,
    string Token);

