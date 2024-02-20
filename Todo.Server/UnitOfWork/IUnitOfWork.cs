using Todo.Server.Repositories.Interfaces;

namespace Todo.Server.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepository { get; }
    ITaskRepository TaskRepository { get; }

    void Commit();
    Task<int> CommitAsync();
    Task DisposeAsync();
}
