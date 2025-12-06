using MakeenCo_Work.Domain.Models;

namespace MakeenCo_Work.Domain.IRepository
{
    public interface IRegulationRepository : IBaseRepository<Regulation>
    {
        Task<List<Regulation>> GetAllAsync();

        Task CreateAsync(string title,string content,bool isActive);

        Task UpdateAsync(Guid id ,string title, string content, bool isActive);
    }
}