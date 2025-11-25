using System.ComponentModel.DataAnnotations;

namespace MakeenCo_Work.Domain.Models
{
	public class TieredDiscount : BaseModel
	{
		[Required]
		public int PeriodDays { get; private set; }

		[Required]
		public int FreeDays { get; private set; }

		public bool IsActive { get; private set; }


		private TieredDiscount() { }

		public TieredDiscount(int periodDays , int freeDays)
		{
			PeriodDays = periodDays;
			FreeDays = freeDays;
			IsActive = true;
		}

		public void Update(int periodDays , int freeDays)
		{
			PeriodDays = periodDays;
			FreeDays = freeDays;
			UpdateTimestamp();
		}

		public void SetActive(bool isActive)
		{
			IsActive = isActive;
			UpdateTimestamp();
		}

		public int GetPaidDays()
		{
			return PeriodDays - FreeDays;
		}
	}
}

