using MakeenCo_Work.Application.Command.DiscountCode;
using MakeenCo_Work.Application.DTOs.DiscountCode;
using MakeenCo_Work.Application.IServices;
using MakeenCo_Work.Domain.IRepository;
using MakeenCo_Work.Domain.Models;

namespace MakeenCo_Work.Application.Services
{
	public class DiscountCodeService : IDiscountCodeService
    {
		private readonly IDiscountCodeRepository _discountCodeRepository;

		public DiscountCodeService(IDiscountCodeRepository discountCodeRepository)
		{
			_discountCodeRepository = discountCodeRepository;
		}


        public async Task<DiscountCodeDto?> GetByIdAsync(Guid id)
        {
            var discountCode = await _discountCodeRepository.GetByIdAsync(id);

            if (discountCode is null)
                return null;

            return MapToDto(discountCode);
        }


        public async Task<DiscountCodeDto?> GeyByCodeAsync(string code)
        {
            var discountCode = await _discountCodeRepository.GetByCodeAsync(code);

            if (discountCode is null)
                return null;

            return MapToDto(discountCode);
        }


        public async Task<IEnumerable<DiscountCodeDto>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            var discountCodes = await _discountCodeRepository.GetAllAsync(pageNumber, pageSize);

            return discountCodes.Select(MapToDto);
        }


        public async Task<IEnumerable<DiscountCodeDto>> GetActiveAsync()
        {
            var discountCode = await _discountCodeRepository.GetActiveAsync();

            return discountCode.Select(MapToDto);
        }


        public async Task<IEnumerable<DiscountCodeDto>> GetExpiredAsync()
        {
            var discountCodes = await _discountCodeRepository.GetExpiredAsync();

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

            await _discountCodeRepository.CreateAsync(discountCode);

            return MapToDto(discountCode);
        }


        public async Task<bool> UpdateAsync(Guid id, UpdateDiscountCodeCommand command)
        {
            var discountCode = await _discountCodeRepository.GetByIdAsync(id);

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

            return await _discountCodeRepository.UpdateAsync(discountCode);
        }


        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _discountCodeRepository.DeleteAsync(id);
        }


        public async Task<bool> ToggleActiveAsync(Guid id)
        {
            var discountCode = await _discountCodeRepository.GetByIdAsync(id);

            if (discountCode is null)
                return false;

            discountCode.SetActive(!discountCode.IsActive);

            return await _discountCodeRepository.UpdateAsync(discountCode);
        }


        public async Task<bool> DeactiveAllAsync()
        {
            return await _discountCodeRepository.DeactiveAllAsync();
        }


        public async Task<int> GetTotalCountAsync()
        {
            return await _discountCodeRepository.GetCountAsync();
        }


        public async Task<bool> ValidateDiscountCodeAsync(string code)
        {
            var discountCode = await _discountCodeRepository.GetByCodeAsync(code);

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

