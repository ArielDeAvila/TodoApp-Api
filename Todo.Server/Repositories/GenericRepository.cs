using Microsoft.EntityFrameworkCore;
using Todo.Server.Data;
using Todo.Server.Data.Entities;
using Todo.Server.Repositories.Interfaces;

namespace Todo.Server.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly TodoContext _context;
    private readonly DbSet<T> _entities;

    public GenericRepository(TodoContext context)
    {
        _context = context;
        _entities = _context.Set<T>();
    }

    public async Task<T?> GetById(int id)
    {
        var entity = await _entities.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);

        return entity;
    }

    public async void Create(T entity)
    {
        await _entities.AddAsync(entity);
    }

    public void Update(T entity)
    {
        _entities.Update(entity);
    }

}
