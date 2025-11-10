using MakeenCo_Work.Application.DTOs;
using MakeenCo_Work.Application.IServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace MakeenCo_Work.Application.Services
{
	public class FileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;


		public FileStorageService(IWebHostEnvironment environment , IConfiguration configuration)
		{
            _environment = environment;
            _configuration = configuration;
		}



        public async Task<List<FileUploadDto>> UploadUserFilesAsync(Guid userId, List<IFormFile> files)
        {
            var uploadedFiles = new List<FileUploadDto>();

            //User Specific Folder
            var uploadPath = Path.Combine(_environment.WebRootPath, "uploads", "users", userId.ToString());

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);


            foreach(var file in files)
            {
                if(file.Length > 0)
                {
                    //Unique File
                    var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    var filepath = Path.Combine(uploadPath, uniqueFileName);


                    //Saving File
                    using (var stream = new FileStream(filepath, FileMode.Create))
                        await file.CopyToAsync(stream);

                    var relativePath = Path.Combine("uploads", "Users", userId.ToString(), uniqueFileName);

                    uploadedFiles.Add(new FileUploadDto
                    {
                        FileName = file.FileName,
                        FilePath = relativePath.Replace("\\" , "/"),
                        FileType = file.ContentType,
                        FileSize = file.Length
                    });
                }
            }

            return uploadedFiles;
        }


        public async Task<bool> DeleteFileAsync(string filePath)
        {
            var fullPath = Path.Combine(_environment.WebRootPath, filePath);

            if(File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);
        }


        public string GetFileUrl(string filePath)
        {
            return $"/{filePath}";
        }
    }
}

