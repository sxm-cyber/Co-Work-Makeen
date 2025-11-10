using MakeenCo_Work.Domain.Models;

namespace MakeenCo_Work.Domain.IRepository
{
	public interface IUserRepository
	{
		Task<User?> GetByIdAsync(Guid id);

		Task<User?> GetByNationalCodeAsync(string nationalCode);

		Task<User?> GetByPhoneNumberAsync(string phoneNumber);

		Task<IEnumerable<User>> GetAllAsync(int pageNumber = 1, int pageSize = 10);

		Task<IEnumerable<User>> GetActiveUsersAsync();

		Task<bool> UpdateAsync(User user);

		Task<bool> DeleteAsync(Guid id);

		Task<int> GetTotalCountAsync();

		Task<bool> ExistsAsync(Guid id);
	}
}

