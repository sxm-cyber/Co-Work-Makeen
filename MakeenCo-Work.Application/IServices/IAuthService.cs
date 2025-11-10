using MakeenCo_Work.Application.Command;
using MakeenCo_Work.Application.DTOs.Auth;
using MakeenCo_Work.Application.DTOs.User;

namespace MakeenCo_Work.Application.IServices
{
	public interface IAuthService
	{
		Task<LoginResponseDto?> AdminLoginAsync(AdminLoginCommand command);

		Task<OtpResponseDto> RequestOtpAsync(RequestOtpCommand command);

		Task<VerifyOtpResponseDto> VerifyOtpAsync(VerifyOtpCommand command);

		Task<LoginResponseDto?> CompleteRegistrationAsync(CompleteRegistrationCommand command);

		Task<bool> LogoutAsync(Guid userId);

		string GenerateJwtToken(UserDto user, IList<string> roles);
	}
}