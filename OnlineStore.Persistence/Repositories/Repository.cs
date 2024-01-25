using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Exeptions;
using OnlineStore.Application.Interfaces;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Domain.Base;

namespace OnlineStore.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity, new()
    {
        protected readonly IApplicationDbContext _context;

        protected DbSet<T> DbSet { get; }

        protected virtual IQueryable<T> Entities => DbSet;

        public bool AutoSaveChanges { get; set; } = true;

        public Repository(IApplicationDbContext context)
        {
            _context = context;
            DbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellation = default) => 
            await Entities.ToArrayAsync(cancellation).ConfigureAwait(false);


        public async Task<bool> ExistsAsync(int id, CancellationToken cancellation = default) => 
            await Entities.AnyAsync(e => e.Id == id, cancellation).ConfigureAwait(false);

        public async Task<T> GetAsync(int id, CancellationToken cancellation = default)
        {
            switch (Entities)
            {
                case DbSet<T> dbSet:
                    return await dbSet.FindAsync(new object[] { id }, cancellation).ConfigureAwait(false) 
                        ?? throw new NotFoundException(nameof(T), id);
                case { } entities:
                    return await entities.FirstOrDefaultAsync(e => e.Id == id, cancellation).ConfigureAwait(false) 
                        ?? throw new NotFoundException(nameof(T), id);
                default:
                    throw new InvalidOperationException("Data source defenition failed.");
            }
        }

        public async Task<T> CreateAsync(T? entity, CancellationToken cancellation = default)
        {
            if (entity is null) throw new ArgumentNullException(nameof(T));
            
            await DbSet.AddAsync(entity, cancellation);
            if (AutoSaveChanges)
                await _context.SaveChangesAsync(cancellation).ConfigureAwait(false);
            return entity;
        }

        public async Task UpdateAsync(T? entity, CancellationToken cancellation = default)
        {
            if (entity is null) throw new ArgumentNullException(nameof(T));

            if (!await ExistsAsync(entity.Id)) throw new NotFoundException(nameof(T), entity.Id);

            DbSet.Update(entity);
            if (AutoSaveChanges)
                await _context.SaveChangesAsync(cancellation).ConfigureAwait(false);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellation = default)
        {
            var entity = await DbSet
                .FindAsync(new object[] { id }, cancellation)
                .ConfigureAwait(false);
            if (entity is not { }) throw new NotFoundException(nameof(T), id);

            DbSet.Remove(entity);
            if (AutoSaveChanges)
                await _context.SaveChangesAsync(cancellation).ConfigureAwait(false);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellation = default)
        {
            return await _context.SaveChangesAsync(cancellation).ConfigureAwait(false);
        }
    }
}
