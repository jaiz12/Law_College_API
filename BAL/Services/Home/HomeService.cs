using Common.DbContext;
using DTO.Models;
using DTO.Models.DataResponse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Home
{
    public class HomeService: MyDbContext , IHomeService
    {
        // ---------------------------------------------
        // GET
        // ---------------------------------------------
        public async Task<DataTable> GetAsync(
            string? PageName)
        {
            try
            {
                OpenContext();

                _sqlCommand.Clear_CommandParameter();

                _sqlCommand.Add_Parameter_WithValue(
                    "PageName",
                    PageName
                );

                var result = await Task.Run(() =>
                    _sqlCommand.Select_Table(
                        "sp_Home_Get",
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
            HomeDTO model)
        {
            try
            {
                OpenContext();

                string message;
                bool status;

                _sqlCommand.Clear_CommandParameter();

                _sqlCommand.Add_Parameter_WithValue(
                    "PageName",
                    model.PageName
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "Icon",
                    model.Icon
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "Title",
                    model.Title
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "Description",
                    model.Description
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "ExternalLink",
                    model.ExternalLink
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "Count",
                    model.Count
                );


                _sqlCommand.Add_Parameter_WithValue(
                    "CreatedBy",
                    model.CreatedBy
                );

                var item = await Task.Run(() =>
                    _sqlCommand.Execute_Query(
                        "sp_Home_Create",
                        CommandType.StoredProcedure
                    )
                );

                if (item)
                {
                    message = $"{model.PageName} Added Successfully";
                    status = true;
                }
                else
                {
                    message = $"Failed to Add {model.PageName}";
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
        public async Task<DataResponse> UpdateAsync(HomeDTO model)
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
                    "PageName",
                    model.PageName
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "Icon",
                    model.Icon
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "Title",
                    model.Title
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "Description",
                    model.Description
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "ExternalLink",
                    model.ExternalLink
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "Count",
                    model.Count
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "UpdatedBy",
                    model.UpdatedBy
                );

                var item = await Task.Run(() =>
                    _sqlCommand.Execute_Query(
                        "sp_Home_Update",
                        CommandType.StoredProcedure
                    )
                );

                if (item)
                {
                    message = $"{model.PageName} Updated Successfully";
                    status = true;
                }
                else
                {
                    message = $"Failed to Update {model.PageName}";
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
            HomeDTO model)
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
                        "sp_Home_Delete",
                        CommandType.StoredProcedure
                    )
                );

                if (item)
                {
                    message =
                        $"{model.PageName} Deleted Successfully.";

                    status = true;
                }
                else
                {
                    message =
                        $"Failed to Delete {model.PageName}";

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
