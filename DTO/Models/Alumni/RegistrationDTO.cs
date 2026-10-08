using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Models.Alumni
{
    public class RegistrationDTO
    {
        public int Id { get; set; }
        public string RegistrationId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string MobileNumber { get; set; } = string.Empty;

        public string CourseProgramme { get; set; } = string.Empty;

        public string BatchGraduationYear { get; set; } = string.Empty;

        public string? CurrentProfessionRole { get; set; }

        public string? CurrentOrganisationChamber { get; set; }

        public string? CurrentCityLocation { get; set; }

        public string? ConnectionPreferences { get; set; }

        public string? LinkedInProfile { get; set; }

        public IFormFile? Photo { get; set; }
        public string? ProfilePhoto { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
