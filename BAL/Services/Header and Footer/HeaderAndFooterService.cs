
using Common.DbContext;
using DTO.Models;
using DTO.Models.DataResponse;
using DTO.Models.Header_and_Footer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Header_and_Footer.Logo_And_Title
{
    internal class HeaderAndFooterService : MyDbContext, IHeaderAndFooterService
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
                        "sp_HeaderAndFooter_Get",
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
            HeaderAndFooterDTO model)
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
                    "LogoPath",
                    model.LogoPath
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "CreatedBy",
                    model.CreatedBy
                );

                var item = await Task.Run(() =>
                    _sqlCommand.Execute_Query(
                        "sp_HeaderAndFooter_Create",
                        CommandType.StoredProcedure
                    )
                );

                if (item)
                {
                    message = $"{model.SectionName} Added successfully.";
                    status = true;
                }
                else
                {
                    message = $"Failed to Add {model.SectionName}";
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
        public async Task<DataResponse> UpdateAsync(HeaderAndFooterDTO model)
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
                    "LogoPath",
                    model.LogoPath
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "UpdatedBy",
                    model.UpdatedBy
                );

                var item = await Task.Run(() =>
                    _sqlCommand.Execute_Query(
                        "sp_HeaderAndFooter_Update",
                        CommandType.StoredProcedure
                    )
                );

                if (item)
                {
                    message = $"{model.SectionName} Updated Successfully";
                    status = true;
                }
                else
                {
                    message = $"Failed to Update {model.SectionName}";
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
            HeaderAndFooterDTO model)
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

                var item = await Task.Run(() =>
                    _sqlCommand.Execute_Query(
                        "sp_HeaderAndFooter_Delete",
                        CommandType.StoredProcedure
                    )
                );

                if (item)
                {
                    message =
                       $"{model.SectionName} deleted successfully.";

                    status = true;
                }
                else
                {
                    message =
                        $"Failed to Delete {model.SectionName}";

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
