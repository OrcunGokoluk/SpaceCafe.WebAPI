using SpaceCafe.Domain.Entities;

namespace SpaceCafe.Application.Authentication.Common;
public record AuthenticationResult(
    User user,
    //Guid Id,
    //string FirstName,
    //string LastName,
    //string Email,
    string Token);

