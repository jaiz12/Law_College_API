
using Microsoft.Extensions.DependencyInjection;

namespace BAL.DependencyResolver
{
    public static class DIResolver
    {

        public static IServiceCollection DIBALResolver(this IServiceCollection services)
        {
            //services.AddScoped<IAccountService, AccountService>();
            return services;
        }
    }
}
