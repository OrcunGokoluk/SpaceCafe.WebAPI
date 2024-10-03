using SpaceCafe.Domain.Entities;

namespace SpaceCafe.Application.Common.Interfaces.Persistance;
public interface IUserRepository
{
    Task<User?> GetUserByEmail(string email); // Asenkron versiyon, Task<User?> döner
    Task Add(User user); // Asenkron ekleme işlemi, Task döner
}
