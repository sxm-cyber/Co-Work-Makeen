using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MakeenCo_Work.Application.Command;
using MakeenCo_Work.Application.DTOs;
using MakeenCo_Work.Application.IServices;
using MakeenCo_Work.Domain.IRepository;
using MakeenCo_Work.Domain.Models;
using MakeenCo_Work.Infrastructure.Repository;

namespace MakeenCo_Work.Application.Services
{
    public class FaqService:IFaqService
    {
        private readonly IFaqRepository _faqRepository;
        public FaqService(IFaqRepository faqRepository)
        {
            _faqRepository = faqRepository;
            
        }

        public async Task CreateFaqAsync(CreateFaqCommand command)
        {
            await _faqRepository.CreateAsync(command.Question,command.Answer,command.PublishInMainPage,command.PublishInFrequentlyAskedQuestions,command.IsActive);
        }

        public async Task DeleteFaqAsync(Guid id)
        {
            await _faqRepository.DeleteAsync(id);
        }

        public async Task<List<FaqDto>> GetAllFaqDtoAsync()
        {
            List<FAQ> faqs = await _faqRepository.GetAllsync();
            List<FaqDto> result = faqs.Select(f => new FaqDto
            {
                Question=f.Question,
                Answer=f.Answer,
                PublishInMainPage=f.PublishInMainPage,
                PublishInFrequentlyAskedQuestions=f.PublishInFrequentlyAskedQuestions,
                IsActive=f.IsActive

            }).ToList();
            return result;
        }

        public async Task UpdateFaqAsync(UpdateFaqCommand command)
        {
            await _faqRepository.UpdateAsync( command.Id,command.Question,command.Answer,command.PublishInMainPage,command.PublishInFrequentlyAskedQuestions,command.IsActive);
        }
    }
}
