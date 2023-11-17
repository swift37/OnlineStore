using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.Domain;

namespace OnlineStore.DAL.Repositories
{
    public static class RepositoriesRegistrator
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services) => services
            .AddScoped<IProductsRepository, ProductsRepository>()
            .AddScoped<IReviewsRepository, ReviewsRepository>()
            .AddScoped<IOrdersRepository, OrdersRepository>()
            .AddScoped<IWishlistsRepository, WishlistsRepository>()
            .AddScoped<IRepository<Category>, Repository<Category>>()
            .AddScoped<IRepository<Coupon>, Repository<Coupon>>()
            .AddScoped<IRepository<Event>, Repository<Event>>()
            .AddScoped<IRepository<ContactRequest>, Repository<ContactRequest>>()
            .AddScoped<IRepository<Subscriber>, Repository<Subscriber>>()
            ;
    }
}
