using Todo.Server.Data.Entities;

namespace Todo.Server.Repositories.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetUserByEmail(string email);
}
