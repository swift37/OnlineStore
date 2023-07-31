using OnlineStore.Services;

namespace Librarian.Services
{
    public static class ServicesRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
            .AddTransient<EmailSenderService>()
            ;
    }
}