using MakeenCo_Work.Domain.Enums;

namespace MakeenCo_Work.Application.Command.DiscountCode
{
	public class CreateDiscountCodeCommand
	{
		public string Code { get; set; }

		public string? Description { get; set; }

		public DiscountType Type { get; set; }

		public decimal Value { get; set; }

		public decimal? MinimumAmount { get; set; }

		public decimal? MaximumDiscount { get; set; }

		public int? UsageLimit { get; set; }

		public DateTime ValidFrom { get; set; }

		public DateTime ValidTo { get; set; }
	}
}

