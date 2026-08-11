using Common.DbContext;
using DTO.Models;
using DTO.Models.DataResponse;
using System.Data;

namespace BAL.Services.Media_and_Gallery.Album
{
    public class AlbumService : MyDbContext, IAlbumService
    {
        // ---------------------------------------
        // Get All Albums
        // ---------------------------------------

        public async Task<DataTable> GetAllAsync()
        {
            try
            {
                OpenContext();

                var result =
                    await Task.Run(() =>
                        _sqlCommand.Select_Table(
                            "sp_MediaGallery_Album_GetAll",
                            CommandType.StoredProcedure
                        )
                    );

                return result;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseContext();
            }
        }


        // ---------------------------------------
        // Get Album By ID
        // ---------------------------------------

        public async Task<DataTable> GetByIdAsync(
            int Id
        )
        {
            try
            {
                OpenContext();

                _sqlCommand.Clear_CommandParameter();

                _sqlCommand.Add_Parameter_WithValue(
                    "Id",
                    Id
                );

                var result =
                    await Task.Run(() =>
                        _sqlCommand.Select_Table(
                            "sp_MediaGallery_Album_GetById",
                            CommandType.StoredProcedure
                        )
                    );

                return result;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseContext();
            }
        }


        // ---------------------------------------
        // Create Album
        // ---------------------------------------

        public async Task<DataResponse> CreateAsync(
            AlbumDTO model
        )
        {
            try
            {
                OpenContext();

                string message = null;
                bool status = false;

                _sqlCommand.Clear_CommandParameter();

                _sqlCommand.Add_Parameter_WithValue(
                    "Name",
                    model.Name
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "Description",
                    model.Description
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "CoverImage",
                    model.CoverImage
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "CreatedBy",
                    model.CreatedBy
                );

                var item =
                    await Task.Run(() =>
                        _sqlCommand.Execute_Query(
                            "sp_MediaGallery_Album_Create",
                            CommandType.StoredProcedure
                        )
                    );

                if (item)
                {
                    message =
                        "Album Added Successfully.";

                    status = true;
                }
                else
                {
                    message =
                        "Failed to Add Album.";

                    status = false;
                }

                return new DataResponse(
                    message,
                    status
                );
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseContext();
            }
        }


        // ---------------------------------------
        // Update Album
        // ---------------------------------------

        public async Task<DataResponse> UpdateAsync(
            AlbumDTO model
        )
        {
            try
            {
                OpenContext();

                string message = null;
                bool status = false;

                _sqlCommand.Clear_CommandParameter();

                _sqlCommand.Add_Parameter_WithValue(
                    "Id",
                    model.Id
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "Name",
                    model.Name
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "Description",
                    model.Description
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "CoverImage",
                    model.CoverImage
                );

                _sqlCommand.Add_Parameter_WithValue(
                    "UpdatedBy",
                    model.UpdatedBy
                );

                var item =
                    await Task.Run(() =>
                        _sqlCommand.Execute_Query(
                            "sp_MediaGallery_Album_Update",
                            CommandType.StoredProcedure
                        )
                    );

                if (item)
                {
                    message =
                        "Album Updated Successfully.";

                    status = true;
                }
                else
                {
                    message =
                        "Failed to Update Album.";

                    status = false;
                }

                return new DataResponse(
                    message,
                    status
                );
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseContext();
            }
        }


        // ---------------------------------------
        // Delete Album
        // ---------------------------------------

        public async Task<DataResponse> DeleteAsync(
            AlbumDTO model
        )
        {
            try
            {
                OpenContext();

                string message = null;
                bool status = false;

                _sqlCommand.Clear_CommandParameter();

                _sqlCommand.Add_Parameter_WithValue(
                    "Id",
                    model.Id
                );

                var item =
                    await Task.Run(() =>
                        _sqlCommand.Execute_Query(
                            "sp_MediaGallery_Album_Delete",
                            CommandType.StoredProcedure
                        )
                    );

                if (item)
                {
                    message =
                        "Album Deleted Successfully.";

                    status = true;
                }
                else
                {
                    message =
                        "Failed to Delete Album.";

                    status = false;
                }

                return new DataResponse(
                    message,
                    status
                );
            }
            catch (Exception)
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