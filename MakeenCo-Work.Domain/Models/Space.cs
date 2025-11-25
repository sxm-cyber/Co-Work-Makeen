using System.ComponentModel.DataAnnotations;

namespace MakeenCo_Work.Domain.Models
{
	public class Space : BaseModel
	{

		[Required, MaxLength(100)]
		public string Name { get; private set; }

		[MaxLength(500)]
		public string? Description { get; private set; }

		public int Capacity { get; private set; }

		public decimal HourlyRate { get; private set; }

		public decimal DailyRate { get; private set; }

		public decimal MonthlyRate { get; private set; }

		public bool IsActive { get; private set; }

		public string? ImageUrl { get; private set; }

		public string? Location { get; private set; }

		public ICollection<Reservation> Reservations { get; private set; } = new List<Reservation>();


		private Space() { }


		public Space(string name , int capacity , decimal hourlyRate, decimal dailyRate, decimal monthlyRate
			,string? description = null , string?  location = null)
		{
			Name = name;
			Capacity = capacity;
			HourlyRate = hourlyRate;
			MonthlyRate = monthlyRate;
			DailyRate = dailyRate;
			Description = description;
			Location = location;
			IsActive = true;
			Reservations = new List<Reservation>();
		}


		public void Update(string name , string? description , int capacity, decimal hourlyRate, decimal dailyRate, decimal monthlyRate , string? location = null)
		{
			Name = name;
			Description = description;
			Capacity = capacity;
			HourlyRate = hourlyRate;
			DailyRate = dailyRate;
			MonthlyRate = monthlyRate;
			Location = location;
			UpdateTimestamp();
		}

		public void UpdateImage(string imageUrl)
		{
			ImageUrl = imageUrl;
            UpdateTimestamp();
        }

		public void SetActive(bool isActive)
		{
			IsActive = isActive;
            UpdateTimestamp();
        }
	}
}

