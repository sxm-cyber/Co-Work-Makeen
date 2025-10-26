using System.ComponentModel.DataAnnotations;

namespace MakeenCo_Work.Domain.Models
{
	public class FAQ
	{

		public Guid Id { get; private set; }


		[Required , MaxLength(500)]
		public string Question { get; private set; }


		[Required]
		public string Answer { get; private set; }

		public int DisplayOrder { get; private set; }

		public bool IsActive { get; private set; }

		public DateTime CreatedAt { get; private set; }

		public DateTime? UpdatedAt { get; private set; }

		public Guid CreatedById { get; private set; }
		public User CreatedBy { get; private set; } = null;


		private FAQ() { }


		public FAQ(string question , string answer , Guid createdById , int displayOrder = 0)
		{
			Id = Guid.NewGuid();
			Question = question;
			Answer = answer;
			CreatedById = createdById;
			DisplayOrder = displayOrder;
			IsActive = true;
			CreatedAt = DateTime.UtcNow;
		}

		public void Update(string question , string answer , int displayOrder)
		{
			Question = question;
			Answer = answer;
			DisplayOrder = displayOrder;
			UpdatedAt = DateTime.UtcNow;
		}

		public void SetActive(bool isActive)
		{
			IsActive = isActive;
			UpdatedAt = DateTime.UtcNow;
		}

		public void UpdateDisplayOrder(int displayOrder)
		{
			DisplayOrder = displayOrder;
			UpdatedAt = DateTime.UtcNow;
		}
		
	}
}

