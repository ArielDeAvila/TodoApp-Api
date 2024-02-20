using Todo.Server.Data.Entities;

namespace Todo.Server.Repositories.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?> GetById(int id);
    void Create(T entity);
    void Update(T entity);

}
