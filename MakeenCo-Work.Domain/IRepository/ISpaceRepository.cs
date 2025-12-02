using MakeenCo_Work.Domain.Models;

namespace MakeenCo_Work.Application.Interfaces
{
    public interface ISpaceRepository
    {
        Task<IEnumerable<Space>> GetAllAsync();
        Task<Space?> GetByIdAsync(Guid id);
        Task AddAsync(Space space);
        void Update(Space space);
        void Delete(Space space);
    }
}
