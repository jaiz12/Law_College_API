
using BAL.Services.About.About_Us;
using BAL.Services.About.Organizational_Structure;
using BAL.Services.About.Recognitions_And_Affiliations;
using Microsoft.Extensions.DependencyInjection;

namespace BAL.DependencyResolver
{
    public static class DIResolver
    {

        public static IServiceCollection DIBALResolver(this IServiceCollection services)
        {
            services.AddScoped<IAboutUsService, AboutUsService>();
            services.AddScoped<IOrganizationalStructureService, OrganizationalStructureService>();
            services.AddScoped<IRecognitionsAndAffiliationsService, RecognitionsAndAffiliationsService>();
            return services;
        }
    }
}
