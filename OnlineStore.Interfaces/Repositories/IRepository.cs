using OnlineStore.Interfaces.Entities;

namespace OnlineStore.Interfaces.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        Task<IEnumerable<T>> GetAll();

        Task<bool> Exists(int id);

        Task<T> Get(int id);

        Task<T> Create(T entity);

        Task Update(T entity);

        Task Delete(T entity);

    }
}
