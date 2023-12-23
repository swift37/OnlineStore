using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Application.Infrastructure;
using OnlineStore.Application.Interfaces.Infrastructure;
using System.Reflection;

namespace OnlineStore.Application
{
    public static class ApplicationRegistrator
    {
        public static IServiceCollection AddApplication(this IServiceCollection services) => services
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
            .AddScoped<IOrderNumbersProvider, OrderNumbersProvider>()
            .AddScoped<IEmailSender, EmailSender>()
            ;
    }
}
