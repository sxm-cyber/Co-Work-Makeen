using MakeenCo_Work.Application.Commands.BlogPosts;
using MakeenCo_Work.Application.DTOs;
using MakeenCo_Work.Application.Interfaces;
using MakeenCo_Work.Domain.Models;

namespace MakeenCo_Work.Application.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IUnitOfWork _unit;

        public BlogPostService(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<IEnumerable<BlogPostDto>> GetAllAsync()
        {
            var posts = await _unit.BlogPosts.GetAllAsync();

            return posts.Select(p => new BlogPostDto
            {
                Id = p.Id,
                Title = p.Title,
                Summary = p.Summary,
                Content = p.Content,
                Category = p.Category,
                FeaturedImageUrl = p.FeaturedImageUrl,
                File = p.File,
                IsPublished = p.IsPublished,
                CreatedAt = p.CreatedAt
            });
        }

        public async Task<BlogPostDto?> GetByIdAsync(Guid id)
        {
            var post = await _unit.BlogPosts.GetByIdAsync(id);

            if (post == null) return null;

            return new BlogPostDto
            {
                Id = post.Id,
                Title = post.Title,
                Summary = post.Summary,
                Content = post.Content,
                Category = post.Category,
                FeaturedImageUrl = post.FeaturedImageUrl,
                File = post.File,
                IsPublished = post.IsPublished,
                CreatedAt = post.CreatedAt
            };
        }

        public async Task<BlogPostDto> CreateAsync(CreateBlogPostCommand c)
        {
            var post = new BlogPost(
                c.Title,
                c.Content,
                c.AuthorId,
                c.Category,
                c.Summary
            );

            await _unit.BlogPosts.AddAsync(post);
            await _unit.CompleteAsync();

            return new BlogPostDto
            {
                Id = post.Id,
                Title = post.Title,
                Summary = post.Summary,
                Content = post.Content,
                Category = post.Category,
                CreatedAt = post.CreatedAt
            };
        }

        public async Task<BlogPostDto?> UpdateAsync(Guid id, UpdateBlogPostCommand c)
        {
            var post = await _unit.BlogPosts.GetByIdAsync(id);
            if (post == null) return null;

            post.Update(c.Title, c.Content, c.Summary);

            await _unit.CompleteAsync();

            return new BlogPostDto
            {
                Id = post.Id,
                Title = post.Title,
                Summary = post.Summary,
                Content = post.Content,
                Category = post.Category,
                CreatedAt = post.CreatedAt
            };
        }

        public async Task<bool> UpdateImageAsync(Guid id, string imageUrl)
        {
            var post = await _unit.BlogPosts.GetByIdAsync(id);
            if (post == null) return false;

            post.UpdateFeatureImage(imageUrl);

            await _unit.CompleteAsync();
            return true;
        }

        public async Task<bool> UpdateFileAsync(Guid id, string filePath)
        {
            var post = await _unit.BlogPosts.GetByIdAsync(id);
            if (post == null) return false;

            post.UpdateFile(filePath);
            await _unit.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var post = await _unit.BlogPosts.GetByIdAsync(id);
            if (post == null) return false;

            _unit.BlogPosts.Delete(post);
            await _unit.CompleteAsync();

            return true;
        }
    }
}
