using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MakeenCo_Work.Domain.Models;

namespace MakeenCo_Work.Domain.IRepository
{
    public interface IRegulationRepository
    {
       
        Task<List<Regulation>> GetAllAsync();
        Task<Regulation?> GetByIdAsync(Guid id);
        Task CreateAsync(string title,string content,bool isActive);
        Task UpdateAsync(Guid id ,string title, string content, bool isActive);
        Task DeleteAsync(Guid id);
        
    }
}
