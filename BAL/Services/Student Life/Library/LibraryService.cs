using Common.DbContext;
using DTO.Models;
using DTO.Models.DataResponse;
using DTO.Models.Student_Life;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Student_Life.Library
{
    public class LibraryService: MyDbContext, ILibraryService
    {

        // ---------------------------------------------
        // GET
        // ---------------------------------------------
        public async Task<DataTable> GetAsync()
        {
            try
            {
                OpenContext();

                var result = await Task.Run(() =>
                    _sqlCommand.Select_Table(
                        "sp_StudentLife_Library_Get",
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
            LibraryDTO model)
        {
            try
            {
                OpenContext();

                string message;
                bool status;

                _sqlCommand.Clear_CommandParameter();

                _sqlCommand.Add_Parameter_WithValue(
                    "Title",
                    model.Title
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "ExternalLink",
                    model.ExternalLink
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "CreatedBy",
                    model.CreatedBy
                );

                var item = await Task.Run(() =>
                    _sqlCommand.Execute_Query(
                        "sp_StudentLife_Library_Create",
                        CommandType.StoredProcedure
                    )
                );

                if (item)
                {
                    message = "Library Added successfully.";
                    status = true;
                }
                else
                {
                    message = "Failed to Add Library";
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
        public async Task<DataResponse> UpdateAsync(LibraryDTO model)
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
                    "Title",
                    model.Title
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "ExternalLink",
                    model.ExternalLink
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "UpdatedBy",
                    model.UpdatedBy
                );

                var item = await Task.Run(() =>
                    _sqlCommand.Execute_Query(
                        "sp_StudentLife_Library_Update",
                        CommandType.StoredProcedure
                    )
                );

                if (item)
                {
                    message = "Library Updated Successfully";
                    status = true;
                }
                else
                {
                    message = "Failed to Update Library";
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
                        "sp_StudentLife_Library_Delete",
                        CommandType.StoredProcedure
                    )
                );

                if (item)
                {
                    message =
                       "Library deleted successfully.";

                    status = true;
                }
                else
                {
                    message =
                        "Failed to Delete Library";

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
