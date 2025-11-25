using System.ComponentModel.DataAnnotations;
using MakeenCo_Work.Domain.Enums;

namespace MakeenCo_Work.Domain.Models
{
	public class Content : BaseModel
	{
		[Required , MaxLength(200)]
		public string Title { get; private set; }

		[MaxLength(1000)]
		public string? Description { get; private set; }

		public string? ContentText { get; private set; }

		public string? ImageUrl { get; private set; }

		public ContentType Type { get; private set; }

		public bool IsActive { get; private set; }

		public bool IsPublished { get; private set; }

		public DateTime? PublishedAt { get; private set; }


		public Guid CreatedById { get; private set; }
		public User CreatedBy { get; private set; } = null;
		


		private Content() { }


		public Content(string title , ContentType type , Guid createdById , string? description = null , string? contentText = null , string? imageUrl = null)
		{
			Title = title;
			Type = type;
            CreatedById = createdById;
            Description = description;
            ContentText = contentText;
            ImageUrl = imageUrl;
            IsActive = true;
            IsPublished = false;
        }

		public void Update(string title , string? description , string? contentText , ContentType type)
		{
			Title = title;
			Description = description;
			ContentText = contentText;
			Type = type;
			UpdateTimestamp();
		}

		public void UpdateImage(string imageUrl)
		{
			ImageUrl = imageUrl;
            UpdateTimestamp();
        }

		public void Publish()
		{
			IsPublished = true;
			PublishedAt = DateTime.UtcNow;
            UpdateTimestamp();
        }

		public void Unpublish()
		{
			IsPublished = false;
            UpdateTimestamp();
        }

		public void SetActive(bool isActive)
		{
			IsActive = isActive;
            UpdateTimestamp();
        }
	}
}

