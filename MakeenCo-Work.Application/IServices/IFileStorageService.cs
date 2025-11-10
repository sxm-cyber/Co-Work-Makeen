using MakeenCo_Work.Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace MakeenCo_Work.Application.IServices
{
	public interface IFileStorageService
	{
		Task<List<FileUploadDto>> UploadUserFilesAsync(Guid userId, List<IFormFile> files);

		Task<bool> DeleteFileAsync(string filePath);

		string GetFileUrl(string filePath);
	}
}

