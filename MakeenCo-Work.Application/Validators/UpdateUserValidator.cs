using FluentValidation;
using MakeenCo_Work.Application.Command.User;

namespace MakeenCo_Work.Application.Validators
{
	public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
	{
		public UpdateUserValidator()
		{
			RuleFor(x => x.FirstName)
				.NotEmpty().WithMessage("FirstName Is Required")
				.MaximumLength(100).WithMessage("FirstName Cannot Exceed 100 Characters");

			RuleFor(x => x.LastName)
				.NotEmpty().WithMessage("LastName Is Required")
				.MaximumLength(100).WithMessage("LastName Cannot Exceed 100 Characters");

			RuleFor(x => x.PhoneNumber)
				.NotEmpty().WithMessage("PhoneNumber Is Required")
				.Matches(@"^09\d{9}$").WithMessage("Invalid Iranian phone number format");
        }
	}
}

