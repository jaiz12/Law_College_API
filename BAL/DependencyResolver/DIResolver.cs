
using BAL.Services.About.About_Us;
using BAL.Services.About.Administrative_Staff;
using BAL.Services.About.Faculty;
using BAL.Services.About.Recognitions_And_Affiliations;
using BAL.Services.About.Statutory_Bodies;
using BAL.Services.Academics.Academic_Calendar;
using BAL.Services.Academics.Our_Program;
using BAL.Services.Committee_and_Cell.Legal_Aid_Cell;
using BAL.Services.ContactUs;
using BAL.Services.Home;
using BAL.Services.Media_and_Gallery.Album;
using BAL.Services.Media_and_Gallery.Media;
using BAL.Services.News_and_Events.Announcemets;
using BAL.Services.Student_Life.Library;
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
            services.AddScoped<ILibraryService, LibraryService>();
            services.AddScoped<ILegalAidCellService, LegalAidCellService>();
            services.AddScoped<IAnnouncementsService, AnnouncementsService>();
            return services;
        }
    }
}
