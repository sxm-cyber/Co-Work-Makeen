using FluentValidation;
using MakeenCo_Work.Application.Command;

namespace MakeenCo_Work.Application.Validators
{
	public class AdminLoginValidator : AbstractValidator<AdminLoginCommand>
	{
		public AdminLoginValidator()
		{
			RuleFor(x => x.Username)
				.NotEmpty().WithMessage("UserName Is Required");


			RuleFor(x => x.Password)
				.NotEmpty().WithMessage("Password Is Required")
				.MinimumLength(6).WithMessage("Password Must Be At Least 6 Characters");
		}
	}
}

