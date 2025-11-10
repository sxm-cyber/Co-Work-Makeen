using MakeenCo_Work.Application.Command.User;

namespace MakeenCo_Work.Application.DTOs.User
{
	public class LoginResponseDto
	{
		public string Token { get; set; }

		public DateTime Expiration { get; set; }

		public UserDto User { get; set; }
	}
}

