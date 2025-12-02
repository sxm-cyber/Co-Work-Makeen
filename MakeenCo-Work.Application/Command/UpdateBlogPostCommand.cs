namespace MakeenCo_Work.Application.Commands.BlogPosts
{
    public class UpdateBlogPostCommand
    {
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string? Summary { get; set; }
    }
}
