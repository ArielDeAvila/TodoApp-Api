using Microsoft.EntityFrameworkCore;
using Todo.Server.Data;
using Todo.Server.Data.Entities;
using Todo.Server.Repositories.Interfaces;

namespace Todo.Server.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly TodoContext _context;
    private readonly DbSet<User> _entities;

    public UserRepository(TodoContext context) : base(context)
    {
        _context = context;
        _entities = _context.Set<User>();
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        var user = await _entities.AsNoTracking().FirstOrDefaultAsync(u => u.Email.Equals(email));

        return user;
    }
}
