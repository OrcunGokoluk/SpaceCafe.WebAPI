using SpaceCafe.Domain.Entities;

namespace SpaceCafe.Application.Common.Interfaces.Persistance;
public interface IUserRepository
{
    User? GetUserByEmail(string email);
    void Add(User user);
}
