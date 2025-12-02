using Microsoft.AspNetCore.Http;

namespace MakeenCo_Work.Application.DTOs
{
    public class BlogPostCreateDto
    {
        public string Title { get; set; }
        public string? Summary { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }

        public Guid AuthorId { get; set; }

        public IFormFile? FeaturedImage { get; set; }
        public IFormFile? File { get; set; } 
    }
}
