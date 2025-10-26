using System;
using System.ComponentModel.DataAnnotations;
using MakeenCo_Work.Domain.Enums;

namespace MakeenCo_Work.Domain.Models
{
	public class DiscountCode
	{
		public Guid Id { get; private set; }

		[Required , MaxLength(50)]
		public string Code { get; private set; }

		[MaxLength(200)]
		public string? Description { get; private set; }

		public DiscountType Type { get; private set; }

		public decimal Value { get; private set; }

		public decimal? MinimumAmount { get; private set; }

		public decimal? MaximumDiscount { get; private set; }

		public int? UsageLimit { get; private set; }

		public int UsedCount { get; private set; }

		public DateTime ValidFrom { get; private set; }

		public DateTime ValidTo { get; private set; }

		public bool IsActive { get; private set; }

		public DateTime CreatedAt { get; private set; }

		public DateTime UpdatedAt { get; private set; }

		public ICollection<Reservation> Reservations { get; private set; } = new List<Reservation>();

		private DiscountCode() { }


		public DiscountCode(string code , DiscountType type , decimal value , DateTime validFrom , DateTime validTo , string? description = null)
		{
			Id = Guid.NewGuid();
			Code = code;
			Type = type;
			Value = value;
			ValidFrom = validFrom;
			ValidTo = validTo;
			Description = description;
			IsActive = true;
			UsedCount = 0;
			CreatedAt = DateTime.UtcNow;
			Reservations = new List<Reservation>();
		}

		public void Update(string code, DiscountType type, decimal value, DateTime validFrom, DateTime validTo, string? description = null)
		{
			Code = code;
            Type = type;
            Value = value;
            ValidFrom = validFrom;
            ValidTo = validTo;
            Description = description;
			UpdatedAt = DateTime.UtcNow;
        }

		public void SetUsageLimit(int? usageLimit)
		{
			UsageLimit = usageLimit;
			UpdatedAt = DateTime.UtcNow;
		}

		public void SetMinimumAmount(decimal? minimumAmount)
		{
			MinimumAmount = minimumAmount;
			UpdatedAt = DateTime.UtcNow;
		}

		public void SetMaximumDiscount(decimal? maximumDiscount)
		{
			MaximumDiscount = maximumDiscount;
			UpdatedAt = DateTime.UtcNow;
		}

		public void SetActive(bool isActive)
		{
			IsActive = isActive;
			UpdatedAt = DateTime.UtcNow;
		}

		public void IncrementUsage()
		{
			UsedCount++;
			UpdatedAt = DateTime.UtcNow;
		}

		public bool IsValid()
		{
			return IsActive && DateTime.UtcNow >= ValidFrom && DateTime.UtcNow <= ValidTo &&
				(UsageLimit == null || UsedCount < UsageLimit);
		}



    }
}

