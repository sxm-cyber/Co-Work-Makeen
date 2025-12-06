using MakeenCo_Work.Domain.IRepository;
using MakeenCo_Work.Domain.Models;
using MakeenCo_Work.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MakeenCo_Work.Infrastructure.Repository
{
	public class TieredDiscountRepository : BaseRepository<TieredDiscount> , ITieredDiscountRepository
    {

		public TieredDiscountRepository(ApplicationDbContext context) : base(context) { }


        public async Task<TieredDiscount?> GetActiveAsync()
        {
            return await _dbSet
                .Where(x => x.IsActive)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync();
        }
    }
}