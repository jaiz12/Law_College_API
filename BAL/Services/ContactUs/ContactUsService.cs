using Common.DbContext;
using DTO.Models;
using DTO.Models.DataResponse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.ContactUs
{
    public class ContactUsService: MyDbContext, IContactUsService 
    {

        // ---------------------------------------------
        // GET
        // ---------------------------------------------
        public async Task<DataTable> GetAsync(
            int Id,
            string? SectionName)
        {
            try
            {
                OpenContext();

                _sqlCommand.Clear_CommandParameter();

                _sqlCommand.Add_Parameter_WithValue(
                    "Id",
                    Id == 0 ? null : Id
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "SectionName",
                    SectionName
                );

                var result = await Task.Run(() =>
                    _sqlCommand.Select_Table(
                        "sp_ContactUs_Get",
                        CommandType.StoredProcedure
                    )
                );

                return result;
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseContext();
            }
        }


        // ---------------------------------------------
        // CREATE
        // ---------------------------------------------
        public async Task<DataResponse> CreateAsync(
            ContactUsDTO model)
        {
            try
            {
                OpenContext();

                string message;
                bool status;

                _sqlCommand.Clear_CommandParameter();

                _sqlCommand.Add_Parameter_WithValue(
                    "SectionName",
                    model.SectionName
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "Icon",
                    model.Icon
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "Detail",
                    model.Detail
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "Name",
                    model.Name
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "Link",
                    model.Link
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "Type",
                    model.Type
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "Latitude",
                    model.Latitude
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "Longitude",
                    model.Longitude
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "CreatedBy",
                    model.CreatedBy
                );

                var item = await Task.Run(() =>
                    _sqlCommand.Execute_Query(
                        "sp_ContactUs_Create",
                        CommandType.StoredProcedure
                    )
                );

                if (item)
                {
                    message = "Contact detail added successfully.";
                    status = true;
                }
                else
                {
                    message = "Failed to add contact detail.";
                    status = false;
                }

                return new DataResponse(
                    message,
                    status
                );
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseContext();
            }
        }


        // ---------------------------------------------
        // UPDATE
        // ---------------------------------------------
        public async Task<DataResponse> UpdateAsync(ContactUsDTO model)
        {
            try
            {
                OpenContext();

                string message;
                bool status;

                _sqlCommand.Clear_CommandParameter();

                _sqlCommand.Add_Parameter_WithValue(
                    "Id",
                    model.Id
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "SectionName",
                    model.SectionName
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "Icon",
                    model.Icon
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "Detail",
                    model.Detail
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "Name",
                    model.Name
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "Link",
                    model.Link
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "Type",
                    model.Type
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "Latitude",
                    model.Latitude
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "Longitude",
                    model.Longitude
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "UpdatedBy",
                    model.UpdatedBy
                );

                var item = await Task.Run(() =>
                    _sqlCommand.Execute_Query(
                        "sp_ContactUs_Update",
                        CommandType.StoredProcedure
                    )
                );

                if (item)
                {
                    message = "Contact detail updated successfully.";
                    status = true;
                }
                else
                {
                    message = "Failed to update contact detail.";
                    status = false;
                }

                return new DataResponse(
                    message,
                    status
                );
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseContext();
            }
        }


        // ---------------------------------------------
        // DELETE
        // ---------------------------------------------
        public async Task<DataResponse> DeleteAsync(
            string Id)
        {
            try
            {
                OpenContext();

                string message;
                bool status;

                _sqlCommand.Clear_CommandParameter();

                _sqlCommand.Add_Parameter_WithValue(
                    "Id",
                    Id
                );

                var item = await Task.Run(() =>
                    _sqlCommand.Execute_Query(
                        "sp_ContactUs_Delete",
                        CommandType.StoredProcedure
                    )
                );

                if (item)
                {
                    message =
                        "Contact detail deleted successfully.";

                    status = true;
                }
                else
                {
                    message =
                        "Failed to delete contact detail.";

                    status = false;
                }

                return new DataResponse(
                    message,
                    status
                );
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseContext();
            }
        }
    }
}
