using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MakeenCo_Work.Domain.Models;

namespace MakeenCo_Work.Domain.IRepository
{
    public interface IFaqRepository
    {
        Task<List<FAQ>> GetAllsync();

        Task<FAQ?> GetByIdAsync(Guid id);

        Task CreateAsync(string question, string answer,  bool isActive, bool publishInMainPage, bool publishInFrequentlyAskedQuestions);

        Task UpdateAsync(Guid Id,string question , string answer,bool PublishInMainPage,bool PublishInFrequentlyAskedQuestions,bool isActive);

        Task DeleteAsync(Guid Id);
       
    }
}
