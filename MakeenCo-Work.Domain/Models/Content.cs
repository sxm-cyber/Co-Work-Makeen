using System;
using System.ComponentModel.DataAnnotations;
using MakeenCo_Work.Domain.Enums;

namespace MakeenCo_Work.Domain.Models
{
	public class Content
	{
		public Guid Id { get; private set; }

		[Required , MaxLength(200)]
		public string Title { get; private set; }

		[MaxLength(1000)]
		public string? Description { get; private set; }

		public string? ContentText { get; private set; }

		public string? ImageUrl { get; private set; }

		public ContentType Type { get; private set; }

		public bool IsActive { get; private set; }

		public bool IsPublished { get; private set; }

		public DateTime CreatedAt { get; private set; }

		public DateTime? UpdatedAt { get; private set; }

		public DateTime? PublishedAt { get; private set; }

		public Guid CreatedById { get; private set; }
		public User CreatedBy { get; private set; } = null;


		private Content() { }


		public Content(string title , ContentType type , Guid createdById , string? description = null , string? contentText = null , string? imageUrl = null)
		{
			Id = Guid.NewGuid();
			Title = title;
			Type = type;
            CreatedById = createdById;
            Description = description;
            ContentText = contentText;
            ImageUrl = imageUrl;
            IsActive = true;
            IsPublished = false;
            CreatedAt = DateTime.UtcNow;
        }

		public void Update(string title , string? description , string? contentText , ContentType type)
		{
			Title = title;
			Description = description;
			ContentText = contentText;
			Type = type;
			UpdatedAt = DateTime.UtcNow;
		}

		public void UpdateImage(string imageUrl)
		{
			ImageUrl = imageUrl;
			UpdatedAt = DateTime.UtcNow;
		}

		public void Publish()
		{
			IsPublished = true;
			PublishedAt = DateTime.UtcNow;
			UpdatedAt = DateTime.UtcNow;
		}

		public void Unpublish()
		{
			IsPublished = false;
			UpdatedAt = DateTime.UtcNow;
		}

		public void SetActive(bool isActive)
		{
			IsActive = isActive;
			UpdatedAt = DateTime.UtcNow;
		}
	}
}

