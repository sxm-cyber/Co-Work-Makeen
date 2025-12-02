using MakeenCo_Work.Application.Interfaces;
using MakeenCo_Work.Domain.Models;
using MakeenCo_Work.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MakeenCo_Work.Infrastructure.Repositories
{
    public class SpaceRepository : ISpaceRepository
    {
        private readonly ApplicationDbContext _Context;

        public SpaceRepository(ApplicationDbContext context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<Space>> GetAllAsync()
            => await _Context.Spaces.ToListAsync();

        public async Task<Space?> GetByIdAsync(Guid id)
            => await _Context.Spaces.FirstOrDefaultAsync(s => s.Id == id);

        public async Task AddAsync(Space space)
            => await _Context.Spaces.AddAsync(space);

        public void Update(Space space)
            => _Context.Spaces.Update(space);

        public void Delete(Space space)
            => _Context.Spaces.Remove(space);
    }
}
