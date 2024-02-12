using OnlineStore.Domain;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Interfaces.Repositories
{
    public interface ISpecificationTypesRepository : IRepository<SpecificationType>
    {
        Task<SpecificationType> GetAsync(
            SpecificationTypeOptions options,
            CancellationToken cancellation = default);
    }
}
