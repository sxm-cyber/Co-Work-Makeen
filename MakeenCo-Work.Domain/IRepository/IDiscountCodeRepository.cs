using MakeenCo_Work.Domain.Models;

namespace MakeenCo_Work.Domain.IRepository
{
	public interface IDiscountCodeRepository
	{
		Task<DiscountCode?> GetByIdAsync(Guid id);

		Task<DiscountCode?> GetByCodeAsync(string code);

		Task<IEnumerable<DiscountCode>> GetAllAsync(int pageNumber = 1, int pageSize = 10);

		Task<IEnumerable<DiscountCode>> GetActiveAsync();

		Task<IEnumerable<DiscountCode>> GetExpiredAsync();

		Task<bool> CreateAsync(DiscountCode discountCode);

		Task<bool> UpdateAsync(DiscountCode discountCode);

		Task<bool> DeleteAsync(Guid id);

		Task<int> GetTotalCountAsync();

		Task<bool> ExistsAsync(Guid id);

		Task<bool> DeactiveAllAsync();
	}
}

