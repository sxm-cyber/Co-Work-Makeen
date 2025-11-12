using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MakeenCo_Work.Domain.IRepository;
using MakeenCo_Work.Domain.Models;
using MakeenCo_Work.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MakeenCo_Work.Infrastructure.Repository
{
    public class RegulationRepository : IRegulationRepository
    {
        private readonly ApplicationDbContext _context;

        public RegulationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(string title, string content, bool isActive)
        {
            var regu = new Regulation(title, content, isActive);
            await _context.Regulations.AddAsync(regu);
            await _context.SaveChangesAsync();
        }



        public async Task<List<Regulation>> GetAllAsync()
        {
            var regu = await _context.Regulations.ToListAsync();
            return regu;
        }

        public async Task<Regulation?> GetByIdAsync(Guid id)
        {
            var regu = await _context.Regulations.FindAsync(id);
            return regu;
        }

        public async Task UpdateAsync(Guid id, string title, string content, bool isActive)
        {
            var regul = await GetByIdAsync(id);
            if (regul != null)
            {
                regul.Update(title, content, isActive);
                await _context.SaveChangesAsync();

            }
        }
        public async Task DeleteAsync(Guid id)
        {
            var regul = await GetByIdAsync(id);
            if (regul != null)
            {
                _context.Regulations.Remove(regul);
                await _context.SaveChangesAsync();
            }
        }
    }
}
