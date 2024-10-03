using SpaceCafe.Application.Common.Interfaces.Persistance;
using SpaceCafe.Domain.Entities;

namespace SpaceCafe.Infrastructure.Persistance;
public class UserRepository : IUserRepository
{
    private static readonly List<User> _users = new();

    public Task Add(User user)
    {
        _users.Add(user);
        return Task.CompletedTask; // Hızlı bir işlem olduğu için sadece Task.CompletedTask döndürüyoruz.
    }

    public Task<User?> GetUserByEmail(string email)
    {
        var user = _users.SingleOrDefault(u => u.Email == email);
        return Task.FromResult(user);
    }
}
