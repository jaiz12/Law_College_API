using Common.DbContext;
using DTO.Models;
using DTO.Models.DataResponse;
using System;
using System.Data;

namespace BAL.Services.Media_and_Gallery.Media
{
    public class MediaService : MyDbContext, IMediaService
    {
        // =====================================================
        // GET ALL MEDIA
        // =====================================================

        public async Task<DataTable> GetAllAsync()
        {
            try
            {
                OpenContext();

                var result =
                    await Task.Run(() =>
                        _sqlCommand.Select_Table(
                            "sp_MediaGallery_Media_GetAll",
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


        // =====================================================
        // GET MEDIA BY ID
        // =====================================================

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
                            "sp_MediaGallery_Media_GetById",
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


        // =====================================================
        // GET MEDIA BY ALBUM ID
        // =====================================================

        public async Task<DataTable> GetByAlbumIdAsync(
            int AlbumId
        )
        {
            try
            {
                OpenContext();

                _sqlCommand.Clear_CommandParameter();

                _sqlCommand.Add_Parameter_WithValue(
                    "AlbumId",
                    AlbumId
                );

                var result =
                    await Task.Run(() =>
                        _sqlCommand.Select_Table(
                            "sp_MediaGallery_Media_GetByAlbumId",
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


        // =====================================================
        // CREATE MEDIA
        // =====================================================

        public async Task<DataResponse> CreateAsync(
            MediaDTO model
        )
        {
            try
            {
                OpenContext();

                _sqlCommand.Clear_CommandParameter();

                // -------------------------------------------------
                // Album ID
                // -------------------------------------------------

                _sqlCommand.Add_Parameter_WithValue(
                    "AlbumId",
                    model.AlbumId
                );

                // -------------------------------------------------
                // Image
                // Controller uploads the file and sets Image
                // when the uploaded file is an image.
                // -------------------------------------------------

                _sqlCommand.Add_Parameter_WithValue(
                    "Image",
                    model.Image
                );

                // -------------------------------------------------
                // Video
                // Controller uploads the file and sets Video
                // when the uploaded file is a video.
                // -------------------------------------------------

                _sqlCommand.Add_Parameter_WithValue(
                    "Video",
                    model.Video
                );

                // -------------------------------------------------
                // Created By
                // -------------------------------------------------

                _sqlCommand.Add_Parameter_WithValue(
                    "CreatedBy",
                    model.CreatedBy
                );

                // -------------------------------------------------
                // Execute
                // -------------------------------------------------

                var item =
                    await Task.Run(() =>
                        _sqlCommand.Execute_Query(
                            "sp_MediaGallery_Media_Create",
                            CommandType.StoredProcedure
                        )
                    );

                if (item)
                {
                    return new DataResponse(
                        "Media Added Successfully.",
                        true
                    );
                }

                return new DataResponse(
                    "Failed to Add Media.",
                    false
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


        // =====================================================
        // UPDATE MEDIA
        // =====================================================

        public async Task<DataResponse> UpdateAsync(
            MediaDTO model
        )
        {
            try
            {
                OpenContext();

                _sqlCommand.Clear_CommandParameter();

                // -------------------------------------------------
                // ID
                // -------------------------------------------------

                _sqlCommand.Add_Parameter_WithValue(
                    "Id",
                    model.Id
                );

                // -------------------------------------------------
                // Album ID
                // -------------------------------------------------

                _sqlCommand.Add_Parameter_WithValue(
                    "AlbumId",
                    model.AlbumId
                );

                // -------------------------------------------------
                // Image
                // -------------------------------------------------

                _sqlCommand.Add_Parameter_WithValue(
                    "Image",
                    model.Image
                );

                // -------------------------------------------------
                // Video
                // -------------------------------------------------

                _sqlCommand.Add_Parameter_WithValue(
                    "Video",
                    model.Video
                );

                // -------------------------------------------------
                // Updated By
                // -------------------------------------------------

                _sqlCommand.Add_Parameter_WithValue(
                    "UpdatedBy",
                    model.UpdatedBy
                );

                // -------------------------------------------------
                // Execute
                // -------------------------------------------------

                var item =
                    await Task.Run(() =>
                        _sqlCommand.Execute_Query(
                            "sp_MediaGallery_Media_Update",
                            CommandType.StoredProcedure
                        )
                    );

                if (item)
                {
                    return new DataResponse(
                        "Media Updated Successfully.",
                        true
                    );
                }

                return new DataResponse(
                    "Failed to Update Media.",
                    false
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


        // =====================================================
        // DELETE MEDIA
        // =====================================================

        public async Task<DataResponse> DeleteAsync(
            MediaDTO model
        )
        {
            try
            {
                OpenContext();

                _sqlCommand.Clear_CommandParameter();

                // -------------------------------------------------
                // ID
                // -------------------------------------------------

                _sqlCommand.Add_Parameter_WithValue(
                    "Id",
                    model.Id
                );

                // -------------------------------------------------
                // Execute
                // -------------------------------------------------

                var item =
                    await Task.Run(() =>
                        _sqlCommand.Execute_Query(
                            "sp_MediaGallery_Media_Delete",
                            CommandType.StoredProcedure
                        )
                    );

                if (item)
                {
                    return new DataResponse(
                        "Media Deleted Successfully.",
                        true
                    );
                }

                return new DataResponse(
                    "Failed to Delete Media.",
                    false
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