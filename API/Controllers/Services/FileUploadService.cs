using DTO.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace API.Controllers.Services
{
    public interface IFileUploadService
    {
        Task<string> UploadAsync(IFormFile file, string module, string submenu);
        void Delete(string filePath);
    }
    public class FileUploadService : IFileUploadService
    {
        private readonly IWebHostEnvironment _env;

        public FileUploadService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> UploadAsync(
            IFormFile file,
            string module,
            string submenu)
        {
            if (file == null || file.Length == 0)
                throw new Exception("No file selected.");

            var extension = Path.GetExtension(file.FileName).ToLower();

            var allowed = new[]
            {
                ".jpg",
                ".jpeg",
                ".png",
                ".gif",
                ".webp",

                ".pdf",

                ".mp4",
                ".avi",
                ".mov",
                ".mkv"
            };

            if (!allowed.Contains(extension))
                throw new Exception("Invalid file type.");

            var root = Path.Combine("assets");

            if (!Directory.Exists(root))
                Directory.CreateDirectory(root);

            string folder;

            if (string.IsNullOrWhiteSpace(submenu))
            {
                folder = Path.Combine(
                    root,
                    module);
            }
            else
            {
                folder = Path.Combine(
                    root,
                    module,
                    submenu);
            }

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var fileName =
                $"{Guid.NewGuid()}{extension}";

            var fullPath = Path.Combine(folder, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);

            await file.CopyToAsync(stream);

            return (string.IsNullOrWhiteSpace(submenu)
                 ? Path.Combine(
                     root,
                     module,
                     fileName)
                 : Path.Combine(
                     root,
                     module,
                     submenu,
                     fileName))
                 .Replace("\\", "/");
        }
        public void Delete(
            string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return;
            }


            // Remove leading slash
            filePath =
                filePath.TrimStart(
                    '/',
                    '\\'
                );


            // Convert URL separators to Windows separators
            filePath =
                filePath.Replace(
                    '/',
                    Path.DirectorySeparatorChar
                );


            var fullPath =
                Path.Combine(
                    filePath
                );


            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }

}
