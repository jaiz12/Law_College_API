
using BAL.Services.About.GeneralOverviewService;
using BAL.Services.About.VisionMission;
using Microsoft.Extensions.DependencyInjection;

namespace BAL.DependencyResolver
{
    public static class DIResolver
    {

        public static IServiceCollection DIBALResolver(this IServiceCollection services)
        {
            services.AddScoped<IGeneralOverviewService, GeneralOverviewService>();
            services.AddScoped<IVisionMissionService, VisionMissionService>();
            return services;
        }
    }
}
