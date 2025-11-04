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
    public class FaqRepository : IFaqRepository
    {
        private ApplicationDbContext _context;
        public FaqRepository(ApplicationDbContext context)
        {
            _context = context;

        }

        public async Task CreateAsync(string question, string answer, bool isActive, bool publishInMainPage, bool publishInFrequentlyAskedQuestions)
        {
            var faq = new FAQ(question, answer, isActive, publishInMainPage, publishInFrequentlyAskedQuestions);
           
            await _context.Faqs.AddAsync(faq);
            await _context.SaveChangesAsync();
        }



        public async Task<List<FAQ>> GetAllsync()
        {
            var faq = await _context.Faqs.ToListAsync();
            return faq;
        }

        public async Task<FAQ?> GetByIdAsync(Guid id)
        {
            var faq = await _context.Faqs.FindAsync(id);
            return faq;
        }

        public async Task UpdateAsync(Guid Id, string question, string answer, bool PublishInMainPage, bool PublishInFrequentlyAskedQuestions, bool isActive)
        {
            var faq = await GetByIdAsync(Id);
            if (faq != null)
            {
                faq.Update(question, answer, PublishInMainPage, PublishInFrequentlyAskedQuestions, isActive);

                await _context.SaveChangesAsync();

            }
        }
        public async Task DeleteAsync(Guid Id)
        {
            var faq = await GetByIdAsync(Id);
            if (faq != null)
            {
                _context.Faqs.Remove(faq);
                await _context.SaveChangesAsync();

            }
        }
    }
}
