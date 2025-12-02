namespace MakeenCo_Work.Application.DTOs
{
    public class BlogPostDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Summary { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }

        public string? FeaturedImageUrl { get; set; }
        public string? File { get; set; }   

        public bool IsPublished { get; set; }
        public int ViewCount { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? PublishedAt { get; set; }
    }
}
