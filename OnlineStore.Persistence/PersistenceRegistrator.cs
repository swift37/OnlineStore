using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Application.Interfaces;
using OnlineStore.Application.Interfaces.Repositories;
using OnlineStore.DAL.Context;
using OnlineStore.DAL.Repositories;
using OnlineStore.Domain.Entities;
using OnlineStore.Persistence.Repositories;

namespace OnlineStore.DAL
{
    public static class PersistenceRegistrator
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration) => services
            .AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DbConnection")))
            .AddScoped<IProductsRepository, ProductsRepository>()
            .AddScoped<ICategoriesRepository, CategoriesRepository>()
            .AddScoped<IReviewsRepository, ReviewsRepository>()
            .AddScoped<IOrdersRepository, OrdersRepository>()
            .AddScoped<IWishlistsRepository, WishlistsRepository>()
            .AddScoped<ISubscribersRepository, SubscribersRepository>()
            .AddScoped<IFilterGroupsRepository, FilterGroupsRepository>()
            .AddScoped<ISpecificationTypesRepository, SpecificationTypesRepository>()
            .AddScoped<ISpecificationsRepository, SpecificationsRepository>()
            .AddScoped<IRepository<Category>, Repository<Category>>()
            .AddScoped<IRepository<Coupon>, Repository<Coupon>>()
            .AddScoped<IRepository<Event>, Repository<Event>>()
            .AddScoped<IRepository<ContactRequest>, Repository<ContactRequest>>()
            .AddScoped<IRepository<MenuItem>, Repository<MenuItem>>()
            .AddScoped<IRepository<NestedMenuItem>, Repository<NestedMenuItem>>()
            .AddScoped<IRepository<ProductTag>, Repository<ProductTag>>()
            .AddScoped<IRepository<PaymentMethod>, Repository<PaymentMethod>>()
            .AddScoped<IRepository<ShippingMethod>, Repository<ShippingMethod>>()
            ;
    }
}
