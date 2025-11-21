using MakeenCo_Work.Application.Command.DiscountCode;
using MakeenCo_Work.Application.DTOs.DiscountCode;

namespace MakeenCo_Work.Application.IServices
{
	public interface ITieredDiscountService
	{
		Task<TieredDiscountDto?> GetActiveAsync();

		Task<TieredDiscountDto> CreateOrUpdateAsync(UpdateTieredDiscountCommand command);

		Task<bool> SetActiveAsync(Guid id, bool isActive);

		Task<int> CalculatePaidDaysAsync(int reservedDays);
	}
}

