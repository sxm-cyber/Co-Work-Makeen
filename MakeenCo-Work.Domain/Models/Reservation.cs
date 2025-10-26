using System.ComponentModel.DataAnnotations;
using MakeenCo_Work.Domain.Enums;

namespace MakeenCo_Work.Domain.Models
{
	public class Reservation
	{
		public Guid Id { get; private set; }

		[Required , MaxLength(50)]
		public string ReservationNumber { get; private set; }

		public DateTime StartDate { get; private set; }

		public DateTime EndDate { get; private set; }

		public DateTime StartTime { get; private set; }

		public DateTime EndTime { get;private set; }

		public ReservationStatus Status { get; private set; }

		public decimal TotalAmount { get; private set; }

		public string? Notes { get; private set; }

		public DateTime CreatedAt { get; private set; }

		public DateTime? UpdatedAt { get; private set; }

		public DateTime? CancelledAt { get; private set; }

		public string? CancellationReason { get; private set; }


		public Guid UserId { get; private set; }
		public User User { get; private set; } = null;

		public Guid SpaceId { get; private set; }
		public Space Space { get; private set; } = null;

		public Guid? DiscountCodeId { get; private set; }
		public DiscountCode? DiscountCode { get; private set; }


		private Reservation() { }

		public Reservation(string reservationNumber , Guid userId , Guid spaceId ,
			DateTime startDate , DateTime endDate , DateTime startTime , DateTime endTime ,
			decimal totalAmount , string? notes = null , Guid? discountCodeId = null)
		{
			Id = Guid.NewGuid();
			ReservationNumber = reservationNumber;
			UserId = userId;
			SpaceId = spaceId;
			StartDate = startDate;
			EndDate = endDate;
			StartTime = startTime;
			EndTime = endTime;
			TotalAmount = totalAmount;
			Notes = notes;
			DiscountCodeId = discountCodeId;
			Status = ReservationStatus.Pending;
			CreatedAt = DateTime.UtcNow;
		}


		public void Confirm()
		{
			Status = ReservationStatus.Confirmed;
			UpdatedAt = DateTime.UtcNow;
		}

		public void Cancel(string reason)
		{
			Status = ReservationStatus.Cancelled;
			CancelledAt = DateTime.UtcNow;
			CancellationReason = reason;
			UpdatedAt = DateTime.UtcNow;
		}

		public void Complete()
		{
			Status = ReservationStatus.Completed;
			UpdatedAt = DateTime.UtcNow;
		}

		public void Update(DateTime startDate, DateTime endDate, DateTime startTime, DateTime endTime, decimal totalAmount, string? notes = null)
		{
			StartDate = startDate;
			EndDate = endDate;
			StartTime = startTime;
			EndTime = endTime;
			TotalAmount = totalAmount;
			Notes = notes;
			UpdatedAt = DateTime.UtcNow;
		}





	}
}

