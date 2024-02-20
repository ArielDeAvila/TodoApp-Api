using Todo.Server.Data.Entities;

namespace Todo.Server.Repositories.Interfaces;

public interface ITaskRepository : IGenericRepository<NoteTask>
{
    Task<IEnumerable<NoteTask>> GetAll(int userId);
    void CompleteTask(int id);
    void RemoveTask(int id);
}
