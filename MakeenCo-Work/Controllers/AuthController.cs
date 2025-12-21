using MakeenCo_Work.Application.Command;
using MakeenCo_Work.Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MakeenCo_Work.Controllers
{
	public class AuthController : BaseApiController
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}


		[HttpPost("AdminLogin"), AllowAnonymous]
		public async Task<IActionResult> AdminLoginAsync([FromBody] AdminLoginCommand command)
		{
			var result = await _authService.AdminLoginAsync(command);

			if (result is null)
				return Unauthorized(new { Message = "UserName Or Password Is InCorrect" });

			return Ok(result);
		}


		[HttpPost("RequestOtp") , AllowAnonymous]
		public async Task<IActionResult> RequestOtpAsync([FromBody] RequestOtpCommand command)
		{
			var result = await _authService.RequestOtpAsync(command);

			return Ok(result);
		}


		[HttpPost("VerifyOtp") , AllowAnonymous]
		public async Task<IActionResult> VerifyOtpAsync([FromBody] VerifyOtpCommand command)
		{
			var result = await _authService.VerifyOtpAsync(command);
			
			if(!result.IsValid)
				return BadRequest(new {Message = result.Message});

			return Ok(result);
		}


		[HttpPost("CompleteRegistration") , AllowAnonymous]
		public async Task<IActionResult> CompleteRegistrationAsync([FromBody] CompleteRegistrationCommand command)
		{
			var result = await _authService.CompleteRegistrationAsync(command);

			return Ok(result);
		}


		[HttpPost("LogOut")] //[Authorize]
		public async Task<IActionResult> LogoutAsync()
		{
			var _ = GetCurrentUserId();

			return Ok(new { Message = "LogOut Successfully" });
		}
	}
}

