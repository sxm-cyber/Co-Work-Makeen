using MakeenCo_Work.Application.Command;
using MakeenCo_Work.Application.Command.User;
using MakeenCo_Work.Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace MakeenCo_Work.Controllers
{

// [Authorize]
	public class UserController : BaseApiController
	{
		private readonly IUserService _userService;
		private readonly IFileStorageService _fileStorageService;
		private readonly IBulkUserService _bulkUserService;


		public UserController(IUserService userService , IFileStorageService fileStorageService, IBulkUserService bulkUserService)
		{
			_userService = userService;
			_fileStorageService = fileStorageService;
			_bulkUserService = bulkUserService;
		}


		[HttpGet("{id}")]
		public async Task<IActionResult> GetByIdAsync(Guid id)
		{
			var user = await _userService.GetByIdAsync(id);

			if (user is null)
				return NotFound();

			return Ok(user);
		}

		
		[HttpGet]
		public async Task<IActionResult> GetAllAsync([FromQuery] int pageNumber = 1 , [FromQuery] int pageSize = 10)
		{
			var users = await _userService.GetAllAsync(pageNumber, pageSize);

			var totalCount = await _userService.GetToTalCountAsync();

			var result = new
			{
				Data = users,
				TotalCount = totalCount,
				PageNumber = pageNumber,
				PageSize = pageSize,
				TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
			};

			return Ok(result);
		}


		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateAsync(Guid id , [FromBody] UpdateUserCommand command)
		{
			var result = await _userService.UpdateAsync(id , command);

			if (!result)
				return NotFound();

			return NoContent();
		}


		[HttpDelete("{id}")] // only Admin Have To Delete
		public async Task<IActionResult> DeleteAsync(Guid id)
		{
			var result = await _userService.DeleteAsync(id);

			if (!result)
				return NotFound();

			return NoContent();
		}


		[HttpPatch("{id}/Activate")]  //[Authorize(Roles = "Admin")]
		public async Task<IActionResult> ActivateAsync(Guid id)
		{
			var result = await _userService.ActivateAsync(id);

			if (!result)
				return NotFound();

			return NoContent();
		}

		
		[HttpPatch("{id}/Deactivate")]  //[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeactivateAsync(Guid id)
		{
			var result = await _userService.DeactivateAsync(id);

			if (!result)
				return NotFound();

			return NoContent();
		}


		[HttpPatch("{id}/SetMandatoryCoworking")] //[Authorize(Roles = "Admin")]
		public async Task<IActionResult> SetMandatoryCoworkingAsync(Guid id , [FromBody] SetMandatoryCoworkingCommand command)
		{
			var result = await _userService.SetMandatoryCoworkingAsync(id , command.IsMandatory);

			if (!result)
				return NotFound();

			return NoContent();
		}


        [HttpPost("RegisterByAdminWithFiles") , Consumes("multipart/form-data")] //[Authorize(Roles = "Admin")]
		public async Task<IActionResult> RegisterByAdminWithFiles([FromForm] AdminRegisterUserCommand command
			, [FromForm] List<IFormFile>? files)
		{
			var user = await _userService.RegisterByAdminAsync(command);

			if (files is not null && files.Count > 0)
			{
				var oploadedFiles = await _fileStorageService.UploadUserFilesAsync(user.Id, files);

				return Ok(new
				{
					User = user,
					uploadedFiles = oploadedFiles
				});
			}

			return Ok(user);
		}


		[HttpPost("BulkUpload")] //[Authorize(Roles = "Admin")]
		public async Task<IActionResult> BulkUploadUsers(IFormFile file)
		{
			if (file is null || file.Length is 0)
				return BadRequest(new { Message = "No File Uploaded" });

			var allowedExtensions = new[] { ".csv", ".txt" };
			var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

			if (!allowedExtensions.Contains(fileExtension))
				return BadRequest(new { Message = " Only CSV File Are Allowed" });

			var result = await _bulkUserService.ProcessBulkUserUploadAsync(file);

			return Ok(new
			{
				Message =
					$"Processed {result.TotalRecords} records . Success : {result.SuccessCount} , Failed : {result.FailureCount}",
				Result = result
			});
		}
    }
}