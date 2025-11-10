namespace MakeenCo_Work.Application.IServices
{
	public interface IOtpService
	{
		Task<string> GenerateOtpAsync(string phoneNumber);

		Task<bool> ValidateOtpAsync(string phoneNumber, string otp);

		Task SendSmsAsync(string phoneNumber, string otp);

		Task MarkPhoneAsVerifiedAsync(string phoneNumber);

		Task<bool> IsPhoneVerifiedAsync(string phoneNumber);

		Task ClearPhoneVerificationAsync(string phoneNumber);
	}
}

