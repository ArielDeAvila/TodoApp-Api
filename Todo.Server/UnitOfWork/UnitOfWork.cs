using Todo.Server.Data;
using Todo.Server.Repositories;
using Todo.Server.Repositories.Interfaces;

namespace Todo.Server.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly TodoContext _context;
    private IUserRepository _userRepository = null!;
    private ITaskRepository _taskRepository = null!;

    public UnitOfWork(TodoContext context)
    {
        _context = context;
    }

    public IUserRepository UserRepository
    {
        get
        {
            return _userRepository ??= new UserRepository(_context);
        }
    }

    public ITaskRepository TaskRepository
    {
        get
        {
            return _taskRepository ??= new TaskRepository(_context);
        }
    }

    public void Commit()
    {
        _context.SaveChanges();
    }

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task DisposeAsync()
    {
        await _context.DisposeAsync();
    }
}
