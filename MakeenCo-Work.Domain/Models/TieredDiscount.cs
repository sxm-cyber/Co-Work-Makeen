using System.ComponentModel.DataAnnotations;

namespace MakeenCo_Work.Domain.Models
{
	public class TieredDiscount
	{
		public Guid Id { get; private set; }

		[Required]
		public int PeriodDays { get; private set; }

		[Required]
		public int FreeDays { get; private set; }

		public bool IsActive { get; private set; }

		public DateTime CreatedAt { get; private set; }

		public DateTime UpdatedAt { get; private set; }


		private TieredDiscount() { }

		public TieredDiscount(int periodDays , int freeDays)
		{
			Id = Guid.NewGuid();
			PeriodDays = periodDays;
			FreeDays = freeDays;
			IsActive = true;
			UpdatedAt = DateTime.UtcNow;
			CreatedAt = DateTime.UtcNow;
		}

		public void Update(int periodDays , int freeDays)
		{
			PeriodDays = periodDays;
			FreeDays = freeDays;
			UpdatedAt = DateTime.UtcNow;
		}

		public void SetActive(bool isActive)
		{
			IsActive = isActive;
			UpdatedAt = DateTime.UtcNow;
		}

		public int GetPaidDays()
		{
			return PeriodDays - FreeDays;
		}
	}
}

