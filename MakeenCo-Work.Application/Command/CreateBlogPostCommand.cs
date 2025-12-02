namespace MakeenCo_Work.Application.Commands.BlogPosts
{
    public class CreateBlogPostCommand
    {
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string Category { get; set; } = null!;
        public Guid AuthorId { get; set; }

        public string? Summary { get; set; }
    }
}
