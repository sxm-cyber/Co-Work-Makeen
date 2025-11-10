namespace MakeenCo_Work.Application.DTOs.Auth
{
	public class OtpResponseDto
	{
		public bool Success { get; set; }

		public string Message { get; set; }

		public int ExpiresInSeconds { get; set; }
	}
}

