using MakeenCo_Work.Domain.IRepository;
using MakeenCo_Work.Domain.Models;
using MakeenCo_Work.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MakeenCo_Work.Infrastructure.Repository
{
	public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

		public BaseRepository(ApplicationDbContext context)
		{
            _context = context;
            _dbSet = context.Set<T>();
		}


        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }


        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }


        public virtual async Task<IEnumerable<T>> GetAllAsync(int pageNumber, int pageSize)
        {
            var offSet = (pageNumber - 1) * pageSize;

            return await _dbSet
                .OrderByDescending(x => x.CreatedAt)
                .Skip(offSet)
                .Take(pageSize)
                .ToListAsync();
        }


        public virtual async Task<bool> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }


        public virtual async Task<bool> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }


        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);

            if (entity is null)
                return false;

            _dbSet.Remove(entity);

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }


        public virtual async Task<bool> ExistsAsync(Guid id)
        {
            return await _dbSet.AnyAsync(x => x.Id == id);
        }


        public async Task<int> GetCountAsync()
        {
            return await _dbSet.CountAsync();
        }
    }
}