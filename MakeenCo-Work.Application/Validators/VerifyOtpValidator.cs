using FluentValidation;
using MakeenCo_Work.Application.Command;

namespace MakeenCo_Work.Application.Validators
{
	public class VerifyOtpValidator : AbstractValidator<VerifyOtpCommand>
	{
		public VerifyOtpValidator()
		{
			RuleFor(x => x.PhoneNumber)
				.NotEmpty().WithMessage("PhoneNumber Is Required")
				.Matches(@"^09\d{9}$").WithMessage("Invalid Iranian PhoneNumber Format");


			RuleFor(x => x.Otp)
				.NotEmpty().WithMessage("OTP Is Required")
				.Length(5).WithMessage("OTP Must Be 5 Digits")
				.Matches(@"^\d{5}$").WithMessage("OTP Must Contain Only Digits");
		}
	}
}

