using System.ComponentModel.DataAnnotations;

namespace MakeenCo_Work.Domain.Models
{
	public class Regulation : BaseModel
	{
		[Required , MaxLength(200)]
		public string Title { get; private set; }

		[Required]
		public string Content { get; private set; }

		public bool IsActive { get; private set; }

		private Regulation() { }


		public Regulation(string title , string content , bool isActive )
		{
			Title = title;
			Content = content;
			IsActive = isActive;
		}

		public void Update(string title , string content,bool isActive)
		{
			Title = title;
			Content = content;
			IsActive = isActive;
			UpdateTimestamp();
		}
	}
}

