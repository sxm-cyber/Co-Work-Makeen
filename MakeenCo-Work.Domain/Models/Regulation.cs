using System.ComponentModel.DataAnnotations;

namespace MakeenCo_Work.Domain.Models
{
	public class Regulation
	{

		public Guid Id { get; private set; }

		[Required , MaxLength(200)]
		public string Title { get; private set; }

		[Required]
		public string Content { get; private set; }

		public bool IsActive { get; private set; }

		public DateTime CreatedAt { get; private set; }

		public DateTime? UpdatedAt { get; private set; }

		public Guid CreatedById { get; private set; }
		public User CreatedBy { get; private set; } = null;



		private Regulation() { }


		public Regulation(string title , string content , Guid createdById  )
		{
			Id = Guid.NewGuid();
			Title = title;
			Content = content;
			CreatedById = createdById;
		
			IsActive = true;
			CreatedAt = DateTime.UtcNow;
		}

		public void Update(string title , string content)
		{
			Title = title;
			Content = content;
			UpdatedAt = DateTime.UtcNow;
		}

		public void SetActive(bool isActive)
		{
			IsActive = isActive;
			UpdatedAt = DateTime.UtcNow;
		}
	}
}

