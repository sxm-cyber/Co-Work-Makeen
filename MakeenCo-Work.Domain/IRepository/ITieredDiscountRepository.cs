using MakeenCo_Work.Domain.Models;

namespace MakeenCo_Work.Domain.IRepository
{
	public interface ITieredDiscountRepository
	{
		Task<TieredDiscount?> GetByIdAsync(Guid id);

		Task<TieredDiscount?> GetActiveAsync();

		Task<bool> CreateAsync(TieredDiscount tieredDiscount);

		Task<bool> UpdateAsync(TieredDiscount tieredDiscount);

		Task<bool> ExistsAsync(Guid id);
	}
}

