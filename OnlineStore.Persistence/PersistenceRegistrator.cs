using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Application.Interfaces;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.DAL.Context;
using OnlineStore.DAL.Repositories;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DAL
{
    public static class PersistenceRegistrator
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration) => services
            .AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("DbConnection"));
                })
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
