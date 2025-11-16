using FluentValidation;
using MakeenCo_Work.Application.Command.Message;

namespace MakeenCo_Work.Application.Validators
{
	public class SendMessageValidator : AbstractValidator<SendMessageCommand>
	{
		public SendMessageValidator()
		{
			RuleFor(x => x.Subject)
				.NotEmpty().WithMessage("Subject Is Required")
				.MaximumLength(500).WithMessage("Subject Cannot Exceed 500 Characters");


			RuleFor(x => x.Content)
				.NotEmpty().WithMessage("Message Content Is Required");


			RuleFor(x => x.RecipientIds)
				.NotEmpty().WithMessage("At Least One Recipient Is Required")
				.When(x => !x.SendToAll);


			RuleFor(x => x.Type)
				.IsInEnum().WithMessage("Invalid Message Type");


			RuleFor(x => x.Priority)
				.IsInEnum().WithMessage("Invalid Message Priority");
		}
	}
}

