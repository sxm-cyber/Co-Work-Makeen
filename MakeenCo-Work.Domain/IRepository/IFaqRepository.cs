using MakeenCo_Work.Domain.Models;

namespace MakeenCo_Work.Domain.IRepository
{
    public interface IFaqRepository : IBaseRepository<FAQ>
    {
        Task<List<FAQ>> GetAllsync();

        Task CreateAsync(string question, string answer,  bool isActive, bool publishInMainPage, bool publishInFrequentlyAskedQuestions);

        Task UpdateAsync(Guid Id,string question , string answer,bool PublishInMainPage,bool PublishInFrequentlyAskedQuestions,bool isActive);
    }
}