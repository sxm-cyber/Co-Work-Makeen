namespace MakeenCo_Work.Application.DTOs.DiscountCode
{
	public class TieredDiscountDto
	{
		public Guid Id { get; set; }

		public int PeriodDays { get; set; }

		public int FreeDays { get; set; }

		public int PaidDays { get; set; } // PeriodDay - FreeDay

		public bool IsActive { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime UpdatedAt { get; set; }
	}
}

