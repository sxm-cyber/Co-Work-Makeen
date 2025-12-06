using MakeenCo_Work.Domain.Models;

namespace MakeenCo_Work.Domain.IRepository
{
	public interface ITieredDiscountRepository : IBaseRepository<TieredDiscount>
	{
		Task<TieredDiscount?> GetActiveAsync();
	}
}