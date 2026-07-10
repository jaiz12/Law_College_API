using ExceptionHandling.LogManagement;
using Microsoft.Extensions.DependencyInjection;
namespace ExceptionHandling.DependencyResolver
{
    public static class DIResolver
    {
        public static IServiceCollection ExceptionDIResolver(this IServiceCollection services)
        {
            services.AddSingleton<ILogManager, LogManager>();
            return services;
        }
    }
}
