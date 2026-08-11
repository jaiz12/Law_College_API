
using BAL.Services.About.About_Us;
using BAL.Services.About.Administrative_Staff;
using BAL.Services.About.Faculty;
using BAL.Services.About.Recognitions_And_Affiliations;
using BAL.Services.About.Statutory_Bodies;
using BAL.Services.ContactUs;
using BAL.Services.Media_and_Gallery.Album;
using Microsoft.Extensions.DependencyInjection;

namespace BAL.DependencyResolver
{
    public static class DIResolver
    {

        public static IServiceCollection DIBALResolver(this IServiceCollection services)
        {
            services.AddScoped<IAboutUsService, AboutUsService>();
            services.AddScoped<IInfrastructureService, InfrastructureService>();
            services.AddScoped<IFacultyService, FacultyService>();
            services.AddScoped<IAdministrativeStaffService, AdministrativeStaffService>();
            services.AddScoped<IRecognitionsAndAffiliationsService, RecognitionsAndAffiliationsService>();
            services.AddScoped<IStatutoryBodiesService, StatutoryBodiesService>();
            services.AddScoped<IContactUsService, ContactUsService>();
            services.AddScoped<IAlbumService, AlbumService>();
            return services;
        }
    }
}
