using SpaceCafe.Domain.Entities;

namespace SpaceCafe.Application.Common.Interfaces.Authentication;
public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
