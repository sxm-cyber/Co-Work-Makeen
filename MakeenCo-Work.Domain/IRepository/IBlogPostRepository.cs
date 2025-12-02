using MakeenCo_Work.Domain.Models;

namespace MakeenCo_Work.Domain.Repositories
{
    public interface IBlogPostRepository
    {
        Task<BlogPost?> GetByIdAsync(Guid id);
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task AddAsync(BlogPost post);
        void Update(BlogPost post);
        void Delete(BlogPost post);
    }
}
