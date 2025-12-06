using MakeenCo_Work.Domain.IRepository;
using MakeenCo_Work.Domain.Models;
using MakeenCo_Work.Infrastructure.Data;

namespace MakeenCo_Work.Infrastructure.Repository
{
    public class RegulationRepository : BaseRepository<Regulation> , IRegulationRepository
    {

        public RegulationRepository(ApplicationDbContext context) : base(context) { }
        

        public async Task CreateAsync(string title, string content, bool isActive)
        {
            var regu = new Regulation(title, content, isActive);

            await CreateAsync(regu);
        }


        public async Task<List<Regulation>> GetAllAsync()
        {
            var regu = await GetAllAsync(1, int.MaxValue);

            return regu.ToList();
        }


        public async Task UpdateAsync(Guid id, string title, string content, bool isActive)
        {
            var regul = await GetByIdAsync(id);

            if (regul is null)
                return;

            regul.Update(title, content, isActive);

            await UpdateAsync(regul);
        }
    }
}