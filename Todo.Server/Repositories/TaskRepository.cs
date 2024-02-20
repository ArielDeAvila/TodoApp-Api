using Microsoft.EntityFrameworkCore;
using Todo.Server.Data;
using Todo.Server.Data.Entities;
using Todo.Server.Repositories.Interfaces;

namespace Todo.Server.Repositories;

public class TaskRepository : GenericRepository<NoteTask>, ITaskRepository
{
    private readonly TodoContext _context;
    private readonly DbSet<NoteTask> _entities;

    public TaskRepository(TodoContext context) : base(context)
    {
        _context = context;
        _entities = _context.Set<NoteTask>();
    }

    public async Task<IEnumerable<NoteTask>> GetAll(int userId)
    {
        var tasks = await _entities
                            .Where(t => t.IsCanceled.Equals(false) && t.UserId.Equals(userId))
                            .AsNoTracking()
                            .ToListAsync();

        return tasks;
    }

    public async void CompleteTask(int id)
    {
        var task = await _entities
                            .AsNoTracking()
                            .FirstOrDefaultAsync(t => t.Id.Equals(id));

        if(task is not null)
        {
            task.IsCompleted = true;
            _entities.Update(task); 
        }


    }

    public async void RemoveTask(int id)
    {
        var task = await _entities
                            .AsNoTracking()
                            .FirstOrDefaultAsync(t => t.Id.Equals(id));

        if (task != null)
        {
            task.IsCanceled = true;
            _entities.Update(task);
        }
    }
}
