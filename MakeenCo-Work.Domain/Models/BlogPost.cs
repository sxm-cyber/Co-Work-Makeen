using System.ComponentModel.DataAnnotations;

namespace MakeenCo_Work.Domain.Models
{
	public class BlogPost : BaseModel
	{
		[Required , MaxLength(200)]
		public string Title { get; private set; }

		[MaxLength(500)]
		public string? Summary { get; private set; }

		[Required]
		public string Content { get; private set; }

		public string  File {  get; private set; }

		public string Category  {  get; private set; }

		public string? FeaturedImageUrl { get; private set; }

		public bool IsPublished { get; private set; }

		public int ViewCount { get; private set; }

		public DateTime? PublishedAt { get; private set; }

		public Guid AuthorId { get; private set; }
		public User Author { get; private set; } = null;


		private BlogPost() { }


		public BlogPost(string title, string content, Guid authorId, string category, string? summary = null, string? featuredImageUrl = null, string file = null)
		{
			Title = title;
			Content = content;
			AuthorId = authorId;
			Summary = summary;
			FeaturedImageUrl = featuredImageUrl;
			IsPublished = false;
			ViewCount = 0;
			File = file;
			Category = category;

        }


		public void Update(string title , string content , string? summary = null )
		{
			Title = title;
			Content = content;
			Summary = summary;
			UpdateTimestamp();
		}


		public void UpdateFeatureImage(string featuredImageUrl)
		{
			FeaturedImageUrl = featuredImageUrl;
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


		public void IncrementViewCount()
		{
			ViewCount++;
            UpdateTimestamp();
        }

        public void UpdateFile(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}

