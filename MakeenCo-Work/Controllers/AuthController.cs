using MakeenCo_Work.Application.Command;
using MakeenCo_Work.Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MakeenCo_Work.Controllers
{
	[ApiController , Route("api/[controller]")]
	public class AuthController : ControllerBase
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
			try
			{
				var result = await _authService.RequestOtpAsync(command);
				return Ok(result);
			}
			catch(Exception ex)
			{
				return BadRequest(new { Message = ex.Message });
			}
		}


		[HttpPost("VerifyOtp") , AllowAnonymous]
		public async Task<IActionResult> VerifyOtpAsync([FromBody] VerifyOtpCommand command)
		{
			try
			{
				var result = await _authService.VerifyOtpAsync(command);

				if (!result.IsValid)
					return BadRequest(new { Message = result.Message });

				return Ok(result);
			}

			catch(Exception ex)
			{
				return BadRequest(new { Message = ex.Message });
			}
		}


		[HttpPost("CompleteRegistration") , AllowAnonymous]
		public async Task<IActionResult> CompleteRegistrationAsync([FromBody] CompleteRegistrationCommand command)
		{
			try
			{
				var result = await _authService.CompleteRegistrationAsync(command);
				return Ok(result);
			}
			catch(Exception ex)
			{
				return BadRequest(new { Message = ex.Message });
			}
		}


		[HttpPost("LogOut")] //[Authorize]
		public async Task<IActionResult> LogoutAsync()
		{
			var idValue = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

			if (string.IsNullOrEmpty(idValue))
				return Unauthorized();

			var userId = Guid.Parse(idValue);

			return Ok(new { Message = "Logout Successfully" });
		}
	}
}

