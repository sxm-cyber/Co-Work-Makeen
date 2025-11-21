using FluentValidation;
using MakeenCo_Work.Application.Command.DiscountCode;

namespace MakeenCo_Work.Application.Validators
{
	public class UpdateDiscountCodeValidator : AbstractValidator<UpdateDiscountCodeCommand>
	{
		public UpdateDiscountCodeValidator()
		{
			RuleFor(x => x.Code)
				.NotEmpty().WithMessage("Discount Code Is Required")
				.MaximumLength(50).WithMessage("Discount Code Must Be Less Than 50 Character");

			RuleFor(x => x.Type)
				.IsInEnum().WithMessage("Invalid Discount Type");

			RuleFor(x => x.Value)
				.GreaterThan(0).WithMessage("Discount Value Must Be Greater Than 0");

			RuleFor(x => x.ValidFrom)
				.NotEmpty().WithMessage("Valid From Date Is Required");

			RuleFor(x => x.ValidTo)
				.NotEmpty().WithMessage("Valid To Date Is Required")
				.GreaterThan(x => x.ValidFrom).WithMessage("Valid To Date Must Be After Valid From Date");

			RuleFor(x => x.UsageLimit)
				.GreaterThan(0).When(x => x.UsageLimit.HasValue)
				.WithMessage("Usage Limit must Be Greeater Than 0");

			RuleFor(x => x.MinimumAmount)
				.GreaterThan(0).When(x => x.MinimumAmount.HasValue)
				.WithMessage("Minimum Amount Must Be Greater tThan 0");

			RuleFor(x => x.MaximumDiscount)
				.GreaterThan(0).When(x => x.MaximumDiscount.HasValue)
				.WithMessage("Maximum Amount Must Be Greater Than 0");
		}
	}
}

