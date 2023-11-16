using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Application.Interfaces.Infrastructure;
namespace OnlineStore.Application.Infrastructure
{
    public static class InfrastructureRegistrator
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services) => services
            .AddScoped<ICartStore, CookiesCartStore>()
            .AddScoped<ICartService, CartService>()
            .AddScoped<IEmailSender, EmailSender>()
            ;
    }
}
