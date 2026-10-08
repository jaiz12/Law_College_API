
using BAL.Services.About.About_Us;
using BAL.Services.About.Administrative_Staff;
using BAL.Services.About.Faculty;
using BAL.Services.About.Recognitions_And_Affiliations;
using BAL.Services.About.Statutory_Bodies;
using BAL.Services.Academics.Academic_Calendar;
using BAL.Services.Academics.Academic_Policies;
using BAL.Services.Academics.Our_Program;
using BAL.Services.Academics.Research_And_Publications;
using BAL.Services.Academics.Syllabus;
using BAL.Services.Admissions.Contact_Admission_Office;
using BAL.Services.Admissions.Eligibility_Admission_Process_And_Intake;
using BAL.Services.Admissions.Prospectus;
using BAL.Services.Admissions.Reservation_Policy;
using BAL.Services.Alumni.Alumni_Events;
using BAL.Services.Alumni.Governing_Body;
using BAL.Services.Alumni.Newsletters;
using BAL.Services.Alumni.Notable_Alumni;
using BAL.Services.Alumni.Registration;
using BAL.Services.Banner;
using BAL.Services.Committee_and_Cell;
using BAL.Services.Committee_and_Cell.Legal_Aid_Cell;
using BAL.Services.Compliance_Or_Disclosures;
using BAL.Services.Examinations.Notifications;
using BAL.Services.Examinations.Results;
using BAL.Services.Examinations.Student_Achievers;
using BAL.Services.Header_and_Footer.Logo_And_Title;
using BAL.Services.Home;
using BAL.Services.Media_and_Gallery.Album;
using BAL.Services.Media_and_Gallery.Media;
using BAL.Services.News_and_Events.Announcemets;
using BAL.Services.Student_Life.Library;
using BAL.Services.Student_Life.Student_Life;
using Microsoft.Extensions.DependencyInjection;

namespace BAL.DependencyResolver
{
    public static class DIResolver
    {

        public static IServiceCollection DIBALResolver(this IServiceCollection services)
        {
            services.AddScoped<IBannerService, BannerService>();
            services.AddScoped<IHomeService, HomeService>();
            services.AddScoped<IAboutUsService, AboutUsService>();
            services.AddScoped<IInfrastructureService, InfrastructureService>();
            services.AddScoped<IFacultyService, FacultyService>();
            services.AddScoped<IAdministrativeStaffService, AdministrativeStaffService>();
            services.AddScoped<IRecognitionsAndAffiliationsService, RecognitionsAndAffiliationsService>();
            services.AddScoped<IStatutoryBodiesService, StatutoryBodiesService>();
            services.AddScoped<IHeaderAndFooterService, HeaderAndFooterService>();
            services.AddScoped<IAlbumService, AlbumService>();
            services.AddScoped<IMediaService, MediaService>();
            services.AddScoped<IOurProgramService, OurProgramService>();
            services.AddScoped<IAcademicCalendarService, AcademicCalendarService>();
            services.AddScoped<ILibraryService, LibraryService>();
            services.AddScoped<ILegalAidCellService, LegalAidCellService>();
            services.AddScoped<IAnnouncementsService, AnnouncementsService>();
            services.AddScoped<ISyllabusService, SyllabusService>();
            services.AddScoped<IResearchAndPublicationsService, ResearchAndPublicationsService>();
            services.AddScoped<IProspectusService, ProspectusService>();
            services.AddScoped<INotificationsService, NotificationsService>();
            services.AddScoped<ICommitteeAndCellService, CommitteeAndCellService>();
            services.AddScoped<IGoverningBodyService, GoverningBodyService>();
            services.AddScoped<IAlumniEventsService, AlumniEventsService>();
            services.AddScoped<INotableAlumniService, NotableAlumniService>();
            services.AddScoped<INewslettersService, NewslettersService>();
            services.AddScoped<IStudentLifeService, StudentLifeService>();
            services.AddScoped<IAcademicPoliciesService, AcademicPoliciesService>();
            services.AddScoped<IContactAdmissionOfficeService, ContactAdmissionOfficeService>();
            services.AddScoped<IResultsService, ResultsService>();
            services.AddScoped<IEligibilityAdmissionProcessAndIntakeService, EligibilityAdmissionProcessAndIntakeService>();
            services.AddScoped<IReservationPolicyService, ReservationPolicyService>();
            services.AddScoped<IComplianceOrDisclosuresService, ComplianceOrDisclosuresService>();
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<IStudentAchieversService, StudentAchieversService>();
            return services;
        }
    }
}
