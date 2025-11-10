using FluentValidation;
using MakeenCo_Work.Application.Command;

namespace MakeenCo_Work.Application.Validators
{
	public class AdminRegisterUserValidator : AbstractValidator<AdminRegisterUserCommand>
	{
		public AdminRegisterUserValidator()
		{
			RuleFor(x => x.FirstName)
				.NotEmpty().WithMessage("FirstName Is Required")
				.MaximumLength(100).WithMessage("FirstName Cannot Exceed 100 Characters");

			RuleFor(x => x.LastName)
				.NotEmpty().WithMessage("LastName Is Required")
				.MaximumLength(100).WithMessage("LastName Cannot Exceed 100 Characters");

			RuleFor(x => x.NationalCode)
				.NotEmpty().WithMessage("NationalCode Is Required")
				.Length(10).WithMessage("NationalCode Must Be 10 Digits")
				.Matches(@"^\d{10}$").WithMessage("NationalCode Must Contain Only Digits");

			RuleFor(x => x.PhoneNumber)
				.NotEmpty().WithMessage("PhoneNumber Is Required")
				.Matches(@"^09\d{9}$").WithMessage("Invalid Iranian PhoneNumber Format");
		}
	}
}

