using MakeenCo_Work.Application.Commands;
using MakeenCo_Work.Application.DTOs;

namespace MakeenCo_Work.Application.Interfaces
{
    public interface ISpaceService
    {
        Task<IEnumerable<SpaceDto>> GetAllAsync();
        Task<SpaceDto?> GetByIdAsync(Guid id);
        Task CreateAsync(CreateSpaceCommand cmd);
        Task UpdateAsync(UpdateSpaceCommand cmd);
        Task SetActive(Guid id, bool isActive);
    }
}
