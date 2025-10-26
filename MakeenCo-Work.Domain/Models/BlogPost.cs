using System.ComponentModel.DataAnnotations;

namespace MakeenCo_Work.Domain.Models
{
	public class BlogPost
	{
		public Guid Id { get; private set; }


		[Required , MaxLength(200)]
		public string Title { get; private set; }


		[MaxLength(500)]
		public string? Summary { get; private set; }


		[Required]
		public string Content { get; private set; }


		public string? FeaturedImageUrl { get; private set; }

		public string? Tags { get; private set; }

		public string? Slug { get; private set; }

		public bool IsPublished { get; private set; }

		public bool IsActive { get; private set; }

		public int ViewCount { get; private set; }

		public DateTime CreatedAt { get; private set; }

		public DateTime? UpdatedAt { get; private set; }

		public DateTime? PublishedAt { get; private set; }

		public Guid AuthorId { get; private set; }
		public User Author { get; private set; } = null;


		private BlogPost() { }


		public BlogPost(string title , string content , Guid authorId , string? summary = null ,string? featuredImageUrl = null , string? tags = null )
		{
			Id = Guid.NewGuid();
			Title = title;
			Content = content;
			AuthorId = authorId;
			Summary = summary;
			FeaturedImageUrl = featuredImageUrl;
			Tags = tags;
			Slug = GenerateSlug(title);
			IsPublished = false;
			IsActive = true;
			ViewCount = 0;
			CreatedAt = DateTime.UtcNow;
		}


		public void Update(string title , string content , string? summary = null , string? tags = null)
		{
			Title = title;
			Content = content;
			Summary = summary;
			Tags = tags;
			Slug = GenerateSlug(title);
			UpdatedAt = DateTime.UtcNow;
		}


		public void UpdateFeatureImage(string featuredImageUrl)
		{
			FeaturedImageUrl = featuredImageUrl;
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

		public void IncrementViewCount()
		{
			ViewCount++;
			UpdatedAt = DateTime.UtcNow;
		}


		private static string GenerateSlug(string title)
		{
			return title.ToLowerInvariant()
				.Replace(" ", "-")
				.Replace("_", "-");
		}
	}
}

