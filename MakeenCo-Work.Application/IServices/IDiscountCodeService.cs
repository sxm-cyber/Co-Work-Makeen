using MakeenCo_Work.Application.Command.DiscountCode;
using MakeenCo_Work.Application.DTOs.DiscountCode;

namespace MakeenCo_Work.Application.IServices
{
	public interface IDiscountCodeService
	{
		Task<DiscountCodeDto?> GetByIdAsync(Guid id);

		Task<DiscountCodeDto?> GeyByCodeAsync(string code);

		Task<IEnumerable<DiscountCodeDto>> GetAllAsync(int pageNumber = 1, int pageSize = 10);

		Task<IEnumerable<DiscountCodeDto>> GetActiveAsync();

		Task<IEnumerable<DiscountCodeDto>> GetExpiredAsync();

		Task<DiscountCodeDto> CreateAsync(CreateDiscountCodeCommand command);

		Task<bool> UpdateAsync(Guid id, UpdateDiscountCodeCommand command);

		Task<bool> DeleteAsync(Guid id);

		Task<bool> ToggleActiveAsync(Guid id);

		Task<bool> DeactiveAllAsync();

		Task<int> GetTotalCountAsync();

		Task<bool> ValidateDiscountCodeAsync(string code);
    }
}

