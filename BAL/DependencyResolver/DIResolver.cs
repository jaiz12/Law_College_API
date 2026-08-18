
using BAL.Services.About.About_Us;
using BAL.Services.About.Administrative_Staff;
using BAL.Services.About.Faculty;
using BAL.Services.About.Recognitions_And_Affiliations;
using BAL.Services.About.Statutory_Bodies;
using BAL.Services.Academics.Academic_Calendar;
using BAL.Services.Academics.Our_Program;
using BAL.Services.ContactUs;
using BAL.Services.Home;
using BAL.Services.Media_and_Gallery.Album;
using BAL.Services.Media_and_Gallery.Media;
using Microsoft.Extensions.DependencyInjection;

namespace BAL.DependencyResolver
{
    public static class DIResolver
    {

        public static IServiceCollection DIBALResolver(this IServiceCollection services)
        {
            services.AddScoped<IHomeService, HomeService>();
            services.AddScoped<IAboutUsService, AboutUsService>();
            services.AddScoped<IInfrastructureService, InfrastructureService>();
            services.AddScoped<IFacultyService, FacultyService>();
            services.AddScoped<IAdministrativeStaffService, AdministrativeStaffService>();
            services.AddScoped<IRecognitionsAndAffiliationsService, RecognitionsAndAffiliationsService>();
            services.AddScoped<IStatutoryBodiesService, StatutoryBodiesService>();
            services.AddScoped<IContactUsService, ContactUsService>();
            services.AddScoped<IAlbumService, AlbumService>();
            services.AddScoped<IMediaService, MediaService>();
            services.AddScoped<IOurProgramService, OurProgramService>();
            services.AddScoped<IAcademicCalendarService, AcademicCalendarService>();
            return services;
        }
    }
}
