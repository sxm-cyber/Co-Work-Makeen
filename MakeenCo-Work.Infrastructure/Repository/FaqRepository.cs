using MakeenCo_Work.Domain.IRepository;
using MakeenCo_Work.Domain.Models;
using MakeenCo_Work.Infrastructure.Data;

namespace MakeenCo_Work.Infrastructure.Repository
{
    public class FaqRepository : BaseRepository<FAQ> , IFaqRepository
    {

        public FaqRepository(ApplicationDbContext context) : base(context) { }
        
        
        public async Task CreateAsync(string question, string answer, bool isActive, bool publishInMainPage, bool publishInFrequentlyAskedQuestions)
        {
            var faq = new FAQ(question, answer, isActive, publishInMainPage, publishInFrequentlyAskedQuestions);

            await CreateAsync(faq);
        }


        public async Task<List<FAQ>> GetAllsync()
        {
            var all = await GetAllAsync();

            return all.ToList();
        }


        public async Task UpdateAsync(Guid Id, string question, string answer, bool PublishInMainPage, bool PublishInFrequentlyAskedQuestions, bool isActive)
        {
            var faq = await GetByIdAsync(Id);
            if (faq != null)
            {
                faq.Update(question, answer, PublishInMainPage, PublishInFrequentlyAskedQuestions, isActive);

                await UpdateAsync(faq);
            }
        }
    }
}