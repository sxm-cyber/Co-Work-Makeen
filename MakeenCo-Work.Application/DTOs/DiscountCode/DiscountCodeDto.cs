using MakeenCo_Work.Domain.Enums;

namespace MakeenCo_Work.Application.DTOs.DiscountCode
{
	public class DiscountCodeDto
	{
		public Guid Id { get; set; }

		public string Code { get; set; }

		public string? Description { get; set; }

		public DiscountType Type { get; set; }

		public decimal Value { get; set; }

		public decimal? MinimumAmount { get; set; }

		public decimal? MaximumAmount { get; set; }

		public int? UsageLimit { get; set; }

		public int UsedCount { get; set; }

		public DateTime ValidFrom { get; set; }

		public DateTime ValidTo { get; set; }

		public bool IsActive { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime UpdatedAt { get; set; }
	}
}

