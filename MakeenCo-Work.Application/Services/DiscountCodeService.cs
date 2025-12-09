using MakeenCo_Work.Application.Command.DiscountCode;
using MakeenCo_Work.Application.DTOs.DiscountCode;
using MakeenCo_Work.Application.Interfaces;
using MakeenCo_Work.Application.IServices;
using MakeenCo_Work.Domain.Models;

namespace MakeenCo_Work.Application.Services
{
	public class DiscountCodeService : IDiscountCodeService
    {
		private readonly IUnitOfWork _unitOfWork;

		public DiscountCodeService(IUnitOfWork unitOfWork)
		{
            _unitOfWork = unitOfWork;
		}


        public async Task<DiscountCodeDto?> GetByIdAsync(Guid id)
        {
            var discountCode = await _unitOfWork.DiscountCodes.GetByIdAsync(id);

            if (discountCode is null)
                return null;

            return MapToDto(discountCode);
        }


        public async Task<DiscountCodeDto?> GeyByCodeAsync(string code)
        {
            var discountCode = await _unitOfWork.DiscountCodes.GetByCodeAsync(code);

            if (discountCode is null)
                return null;

            return MapToDto(discountCode);
        }


        public async Task<IEnumerable<DiscountCodeDto>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            var discountCodes = await _unitOfWork.DiscountCodes.GetAllAsync(pageNumber, pageSize);

            return discountCodes.Select(MapToDto);
        }


        public async Task<IEnumerable<DiscountCodeDto>> GetActiveAsync()
        {
            var discountCode = await _unitOfWork.DiscountCodes.GetActiveAsync();

            return discountCode.Select(MapToDto);
        }


        public async Task<IEnumerable<DiscountCodeDto>> GetExpiredAsync()
        {
            var discountCodes = await _unitOfWork.DiscountCodes.GetExpiredAsync();

            return discountCodes.Select(MapToDto);
        }


        public async Task<DiscountCodeDto> CreateAsync(CreateDiscountCodeCommand command)
        {
            var discountCode = new DiscountCode(
                command.Code,
                command.Type,
                command.Value,
                command.ValidFrom,
                command.ValidTo,
                command.Description
                );

            if (command.MinimumAmount.HasValue)
                discountCode.SetMinimumAmount(command.MinimumAmount.Value);

            if (command.MaximumDiscount.HasValue)
                discountCode.SetMaximumDiscount(command.MaximumDiscount.Value);

            if (command.UsageLimit.HasValue)
                discountCode.SetUsageLimit(command.UsageLimit.Value);

            await _unitOfWork.DiscountCodes.CreateAsync(discountCode);

            await _unitOfWork.CompleteAsync();

            return MapToDto(discountCode);
        }


        public async Task<bool> UpdateAsync(Guid id, UpdateDiscountCodeCommand command)
        {
            var discountCode = await _unitOfWork.DiscountCodes.GetByIdAsync(id);

            if (discountCode is null)
                return false;

            discountCode.Update(
                command.Code,
                command.Type,
                command.Value,
                command.ValidFrom,
                command.ValidTo,
                command.Description
                );

            if (command.MinimumAmount.HasValue)
                discountCode.SetMinimumAmount(command.MinimumAmount.Value);

            else
                discountCode.SetMinimumAmount(null);

            if (command.MaximumDiscount.HasValue)
                discountCode.SetMaximumDiscount(command.MaximumDiscount.Value);

            else
                discountCode.SetMaximumDiscount(null);

            if (command.UsageLimit.HasValue)
                discountCode.SetUsageLimit(command.UsageLimit.Value);

            else
                discountCode.SetUsageLimit(null);

            await _unitOfWork.DiscountCodes.UpdateAsync(discountCode);

            await _unitOfWork.CompleteAsync();

            return true;
        }


        public async Task<bool> DeleteAsync(Guid id)
        {
            await _unitOfWork.DiscountCodes.DeleteAsync(id);

            await _unitOfWork.CompleteAsync();

            return true;
        }


        public async Task<bool> ToggleActiveAsync(Guid id)
        {
            var discountCode = await _unitOfWork.DiscountCodes.GetByIdAsync(id);

            if (discountCode is null)
                return false;

            discountCode.SetActive(!discountCode.IsActive);

            await _unitOfWork.DiscountCodes.UpdateAsync(discountCode);

            await _unitOfWork.CompleteAsync();

            return true;
        }


        public async Task<bool> DeactiveAllAsync()
        {
            return await _unitOfWork.DiscountCodes.DeactiveAllAsync();
        }


        public async Task<int> GetTotalCountAsync()
        {
            return await _unitOfWork.DiscountCodes.GetCountAsync();
        }


        public async Task<bool> ValidateDiscountCodeAsync(string code)
        {
            var discountCode = await _unitOfWork.DiscountCodes.GetByCodeAsync(code);

            if (discountCode is null)
                return false;

            return discountCode.IsValid();
        }


        private DiscountCodeDto MapToDto(DiscountCode discountCode)
		{
			return new DiscountCodeDto
			{
				Id = discountCode.Id,
				Code = discountCode.Code,
				Description = discountCode.Description,
				Type = discountCode.Type,
				Value = discountCode.Value,
				MaximumAmount = discountCode.MaximumDiscount,
				MinimumAmount = discountCode.MinimumAmount,
				UsageLimit = discountCode.UsageLimit,
				ValidFrom = discountCode.ValidFrom,
				ValidTo = discountCode.ValidTo,
				IsActive = discountCode.IsActive,
				CreatedAt = discountCode.CreatedAt,
				UpdatedAt = discountCode.UpdatedAt
			};
		}
	}
}