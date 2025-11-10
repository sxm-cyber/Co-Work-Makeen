using MakeenCo_Work.Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace MakeenCo_Work.Application.IServices
{
	public interface IBulkUserService
	{
		Task<BulkUploadResultDto> ProcessBulkUserUploadAsync(IFormFile file);
	}
}

