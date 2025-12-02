using MakeenCo_Work.Application.Commands.BlogPosts;
using MakeenCo_Work.Application.DTOs;

namespace MakeenCo_Work.Application.Interfaces
{
    public interface IBlogPostService
    {
        Task<IEnumerable<BlogPostDto>> GetAllAsync();
        Task<BlogPostDto?> GetByIdAsync(Guid id);

        Task<BlogPostDto> CreateAsync(CreateBlogPostCommand command);
        Task<BlogPostDto?> UpdateAsync(Guid id, UpdateBlogPostCommand command);

        Task<bool> UpdateImageAsync(Guid id, string imageUrl);
        Task<bool> UpdateFileAsync(Guid id, string filePath);

        Task<bool> DeleteAsync(Guid id);
    }
}
