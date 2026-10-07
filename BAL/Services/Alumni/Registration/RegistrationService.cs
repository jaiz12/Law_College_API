using Common.DbContext;
using DTO.Models.Alumni;
using DTO.Models.DataResponse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Alumni.Registration
{
    public class RegistrationService: MyDbContext, IRegistrationService
    {

        public async Task<DataTable> GetAllAsync()
        {
            try
            {
                OpenContext();
                var result = await Task.Run(() => _sqlCommand.Select_Table("sp_Alumni_Registration_Get", CommandType.StoredProcedure));
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseContext();
            }
        }


        public async Task<DataResponse> CreateAsync(
          RegistrationDTO model)
        {
            try
            {
                OpenContext();
                string message = null;
                bool status = false;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("FullName", model.FullName);
                _sqlCommand.Add_Parameter_WithValue("Email", model.Email);
                _sqlCommand.Add_Parameter_WithValue("MobileNumber", model.MobileNumber);
                _sqlCommand.Add_Parameter_WithValue("CourseProgramme", model.CourseProgramme);
                _sqlCommand.Add_Parameter_WithValue("BatchGraduationYear", model.BatchGraduationYear);
                _sqlCommand.Add_Parameter_WithValue("CurrentProfessionRole", model.CurrentProfessionRole);
                _sqlCommand.Add_Parameter_WithValue("CurrentOrganisationChamber", model.CurrentOrganisationChamber);
                _sqlCommand.Add_Parameter_WithValue("CurrentCityLocation", model.CurrentCityLocation);
                _sqlCommand.Add_Parameter_WithValue("ConnectionPreferences", model.ConnectionPreferences);
                _sqlCommand.Add_Parameter_WithValue("LinkedInProfile", model.LinkedInProfile);
                _sqlCommand.Add_Parameter_WithValue("ProfilePhoto", model.ProfilePhoto);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("sp_Alumni_Registration_Create", CommandType.StoredProcedure));
                if (item)
                {
                    message = "Registered Successfully";
                    status = true;
                }
                else
                {
                    message = "Failed to Register";
                    status = false;
                }
                return new DataResponse(message, status);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseContext();
            }
        }
    }
}
