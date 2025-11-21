using MakeenCo_Work.Application.Command.DiscountCode;
using MakeenCo_Work.Application.DTOs.DiscountCode;
using MakeenCo_Work.Application.IServices;
using MakeenCo_Work.Domain.IRepository;
using MakeenCo_Work.Domain.Models;

namespace MakeenCo_Work.Application.Services
{
	public class TieredDiscountService : ITieredDiscountService
    {
		private readonly ITieredDiscountRepository _tieredDiscountRepository;

		public TieredDiscountService(ITieredDiscountRepository tieredDiscountRepository)
		{
			_tieredDiscountRepository = tieredDiscountRepository;
		}


        public async Task<TieredDiscountDto?> GetActiveAsync()
        {
			var tieredDiscount = await _tieredDiscountRepository.GetActiveAsync();

			if (tieredDiscount is null)
				return null;

			return MapToDto(tieredDiscount);
        }


        public async Task<TieredDiscountDto> CreateOrUpdateAsync(UpdateTieredDiscountCommand command)
        {
			var existing = await _tieredDiscountRepository.GetActiveAsync();

			if(existing is null)
			{
				var tieredDiscount = new TieredDiscount(command.PeriodDays, command.FreeDays);

				await _tieredDiscountRepository.CreateAsync(tieredDiscount);

				return MapToDto(tieredDiscount);
			}
			else
			{
				existing.Update(command.PeriodDays, command.FreeDays);

				await _tieredDiscountRepository.UpdateAsync(existing);

				return MapToDto(existing);
			}
        }


        public async Task<bool> SetActiveAsync(Guid id, bool isActive)
        {
			var tieredDiscount = await _tieredDiscountRepository.GetByIdAsync(id);

			if (tieredDiscount is null)
				return false;

			tieredDiscount.SetActive(isActive);

			return await _tieredDiscountRepository.UpdateAsync(tieredDiscount);
        }


        public async Task<int> CalculatePaidDaysAsync(int reservedDays)
        {
			var tieredDiscount = await _tieredDiscountRepository.GetActiveAsync();

			if (tieredDiscount is null || !tieredDiscount.IsActive)
				return reservedDays;

			// Discount ONLY applies when reserved days EXACTLY equals period days (e.g., 18 days)
			if (reservedDays == tieredDiscount.PeriodDays)
				return reservedDays - tieredDiscount.FreeDays;

			// For any other number of days, pay for all reserved days
			return reservedDays;
        }


        private TieredDiscountDto MapToDto(TieredDiscount tieredDiscount)
		{
			return new TieredDiscountDto
			{
				Id = tieredDiscount.Id,
				PeriodDays = tieredDiscount.PeriodDays,
				FreeDays = tieredDiscount.FreeDays,
				PaidDays = tieredDiscount.GetPaidDays(),
				IsActive = tieredDiscount.IsActive,
				CreatedAt = tieredDiscount.CreatedAt,
				UpdatedAt = tieredDiscount.UpdatedAt
			};
		}
	}
}

