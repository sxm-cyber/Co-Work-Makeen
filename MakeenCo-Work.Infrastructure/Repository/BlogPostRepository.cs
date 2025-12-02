using MakeenCo_Work.Domain.Models;
using MakeenCo_Work.Domain.Repositories;
using MakeenCo_Work.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace MakeenCo_Work.Infrastructure.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext _context;

        public BlogPostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(BlogPost post)
        {
            await _context.BlogPosts.AddAsync(post);
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await _context.BlogPosts.ToListAsync();
        }

        public async Task<BlogPost?> GetByIdAsync(Guid id)
        {
            return await _context.BlogPosts
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(BlogPost post)
        {
            _context.BlogPosts.Update(post);
        }

        public void Delete(BlogPost post)
        {
            _context.BlogPosts.Remove(post);
        }
    }
}
