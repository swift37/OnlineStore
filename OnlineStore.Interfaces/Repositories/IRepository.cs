﻿using OnlineStore.Domain.Base;

namespace OnlineStore.Interfaces.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        Task<IEnumerable<T>> GetAll(CancellationToken cancellation = default);

        Task<bool> Exists(int id, CancellationToken cancellation = default);

        Task<T?> Get(int id, CancellationToken cancellation = default);

        Task<T?> Create(T? entity, CancellationToken cancellation = default);

        Task Update(T? entity, CancellationToken cancellation = default);

        Task<bool> Delete(int id, CancellationToken cancellation = default);

    }
}
