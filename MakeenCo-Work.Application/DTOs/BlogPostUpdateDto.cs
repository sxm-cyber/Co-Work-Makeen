using Microsoft.AspNetCore.Http;

namespace MakeenCo_Work.Application.DTOs
{
    public class BlogPostUpdateDto
    {
        public string Title { get; set; }
        public string? Summary { get; set; }
        public string Content { get; set; }

        public string Category { get; set; }

        public IFormFile? FeaturedImage { get; set; }
        public IFormFile? File { get; set; }
    }
}
