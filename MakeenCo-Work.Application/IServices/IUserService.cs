using MakeenCo_Work.Application.Command;
using MakeenCo_Work.Application.Command.User;
using MakeenCo_Work.Application.DTOs.User;

namespace MakeenCo_Work.Application.IServices
{
	public interface IUserService
	{
		Task<UserDto> RegisterByAdminAsync(AdminRegisterUserCommand command);

		Task<UserDto?> GetByIdAsync(Guid id);

		Task<UserDto?> GetByNationalCodeAsync(string nationalCode);

		Task<IEnumerable<UserDto>> GetAllAsync(int pageNumber = 1, int pageSize = 10);

		Task<bool> UpdateAsync(Guid id , UpdateUserCommand updateUserCommand);

		Task<bool> DeleteAsync(Guid id);

		Task<bool> ActivateAsync(Guid id);

		Task<bool> DeactivateAsync(Guid id);

		Task<bool> SetMandatoryCoworkingAsync(Guid userId, bool isMandatory);


        Task<int> GetToTalCountAsync();
	}
}

