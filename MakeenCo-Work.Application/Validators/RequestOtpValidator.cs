using FluentValidation;
using MakeenCo_Work.Application.Command;

namespace MakeenCo_Work.Application.Validators
{
	public class RequestOtpValidator : AbstractValidator<RequestOtpCommand>
	{
		public RequestOtpValidator()
		{
			RuleFor(x => x.PhoneNumber)
				.NotEmpty().WithMessage("PhoneNumber Is Required")
				.Matches(@"^09\d{9}$").WithMessage("Invalid Iranian Phone Number Form");
		}
	}
}

