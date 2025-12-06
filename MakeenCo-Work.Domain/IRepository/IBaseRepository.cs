using MakeenCo_Work.Domain.Models;

namespace MakeenCo_Work.Domain.IRepository
{
	public interface IBaseRepository<T> where T : BaseModel
	{
		Task<T?> GetByIdAsync(Guid id);

		Task<IEnumerable<T>> GetAllAsync();

		Task<IEnumerable<T>> GetAllAsync(int pageNumber, int pageSize);

		Task<bool> CreateAsync(T entity);

		Task<bool> UpdateAsync(T entity);

		Task<bool> DeleteAsync(Guid id);

		Task<bool> ExistsAsync(Guid id);

		Task<int> GetCountAsync();
	}
}

