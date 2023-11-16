using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.DAL.Context;
using OnlineStore.Domain.Base;

namespace OnlineStore.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity, new()
    {
        protected readonly ApplicationDbContext _context;

        protected DbSet<T> DbSet { get; }

        protected virtual IQueryable<T> Entities => DbSet;

        public bool AutoSaveChanges { get; set; } = true;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            DbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll(CancellationToken cancellation = default) => 
            await Entities.ToArrayAsync(cancellation).ConfigureAwait(false);


        public async Task<bool> Exists(int id, CancellationToken cancellation = default) => 
            await Entities.AnyAsync(e => e.Id == id, cancellation).ConfigureAwait(false);

        public async Task<T?> Get(int id, CancellationToken cancellation = default)
        {
            switch (Entities)
            {
                case DbSet<T> dbSet:
                    return await dbSet.FindAsync(new object[] { id }, cancellation).ConfigureAwait(false);
                case { } entities:
                    return await entities.FirstOrDefaultAsync(e => e.Id == id, cancellation).ConfigureAwait(false);
                default:
                    throw new InvalidOperationException("Data source defenition failed.");
            }
        }

        public async Task<T?> Create(T? entity, CancellationToken cancellation = default)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            DbSet.Entry(entity).State = EntityState.Added;
            if (AutoSaveChanges)
                await _context.SaveChangesAsync(cancellation).ConfigureAwait(false);
            return entity;
        }

        public async Task Update(T? entity, CancellationToken cancellation = default)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            DbSet.Entry(entity).State = EntityState.Modified;
            if (AutoSaveChanges)
                await _context.SaveChangesAsync(cancellation).ConfigureAwait(false);
        }

        public async Task<bool> Delete(int id, CancellationToken cancellation = default)
        {
            var entity = await DbSet
                .FindAsync(new object[] { id }, cancellation)
                .ConfigureAwait(false);
            if (entity is not { }) return false;

            DbSet.Entry(entity).State = EntityState.Deleted;
            if (AutoSaveChanges)
                await _context.SaveChangesAsync(cancellation).ConfigureAwait(false);
            return true;
        }

        public async Task<int> SaveChanges(CancellationToken cancellation = default)
        {
            return await _context.SaveChangesAsync(cancellation).ConfigureAwait(false);
        }
    }
}
