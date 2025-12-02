using FluentValidation;
using MakeenCo_Work.Application.Command.DiscountCode;

namespace MakeenCo_Work.Application.Validators
{
	public class UpdateTieredDiscountValidator : AbstractValidator<UpdateTieredDiscountCommand>
	{
		public UpdateTieredDiscountValidator()
		{
			RuleFor(x => x.PeriodDays)
				.GreaterThan(0).WithMessage("Period Days Must Be Greater Than 0");

			RuleFor(x => x.FreeDays)
				.GreaterThanOrEqualTo(0).WithMessage("Free Days Must Be Greater Than Or Equal To 0")
				.LessThan(x => x.PeriodDays).WithMessage("Free Days Must Be Less Than Period Days");
		}
	}
}

