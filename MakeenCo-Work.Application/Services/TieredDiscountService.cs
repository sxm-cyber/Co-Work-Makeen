using MakeenCo_Work.Application.Command.DiscountCode;
using MakeenCo_Work.Application.DTOs.DiscountCode;
using MakeenCo_Work.Application.Interfaces;
using MakeenCo_Work.Application.IServices;
using MakeenCo_Work.Domain.Models;

namespace MakeenCo_Work.Application.Services
{
	public class TieredDiscountService : ITieredDiscountService
    {
		private readonly IUnitOfWork _unitOfWork;

		public TieredDiscountService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}


        public async Task<TieredDiscountDto?> GetActiveAsync()
        {
			var tieredDiscount = await _unitOfWork.TieredDiscounts.GetActiveAsync();

			if (tieredDiscount is null)
				return null;

			return MapToDto(tieredDiscount);
        }


        public async Task<TieredDiscountDto> CreateOrUpdateAsync(UpdateTieredDiscountCommand command)
        {
			var existing = await _unitOfWork.TieredDiscounts.GetActiveAsync();

			if(existing is null)
			{
				var tieredDiscount = new TieredDiscount(command.PeriodDays, command.FreeDays);

				await _unitOfWork.TieredDiscounts.CreateAsync(tieredDiscount);

				await _unitOfWork.CompleteAsync();

				return MapToDto(tieredDiscount);
			}
			else
			{
				existing.Update(command.PeriodDays, command.FreeDays);

				await _unitOfWork.TieredDiscounts.UpdateAsync(existing);

				await _unitOfWork.CompleteAsync();

				return MapToDto(existing);
			}
        }


        public async Task<bool> SetActiveAsync(Guid id, bool isActive)
        {
			var tieredDiscount = await _unitOfWork.TieredDiscounts.GetByIdAsync(id);

			if (tieredDiscount is null)
				return false;

			tieredDiscount.SetActive(isActive);

			await _unitOfWork.TieredDiscounts.UpdateAsync(tieredDiscount);

			await _unitOfWork.CompleteAsync();

			return true;
        }


        public async Task<int> CalculatePaidDaysAsync(int reservedDays)
        {
			var tieredDiscount = await _unitOfWork.TieredDiscounts.GetActiveAsync();

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