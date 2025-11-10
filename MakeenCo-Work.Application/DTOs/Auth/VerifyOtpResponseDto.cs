using MakeenCo_Work.Application.DTOs.User;

namespace MakeenCo_Work.Application.DTOs.Auth
{
	public class VerifyOtpResponseDto
	{
		public bool IsValid { get; set; }

		public bool UserExists { get; set; }

		public string? Token { get; set; }

		public UserDto? User { get; set; }

		public string? Message { get; set; }
	}
}

