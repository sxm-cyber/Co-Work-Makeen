using MakeenCo_Work.Domain.Models;

namespace MakeenCo_Work.Domain.IRepository
{
	public interface IDiscountCodeRepository : IBaseRepository<DiscountCode>
	{
		Task<DiscountCode?> GetByCodeAsync(string code);

		Task<IEnumerable<DiscountCode>> GetActiveAsync();

		Task<IEnumerable<DiscountCode>> GetExpiredAsync();

		Task<bool> DeactiveAllAsync();
	}
}

