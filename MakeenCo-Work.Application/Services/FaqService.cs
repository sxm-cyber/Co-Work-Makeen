using MakeenCo_Work.Application.Command;
using MakeenCo_Work.Application.DTOs;
using MakeenCo_Work.Application.Interfaces;
using MakeenCo_Work.Application.IServices;
using MakeenCo_Work.Domain.Models;

namespace MakeenCo_Work.Application.Services
{
    public class FaqService:IFaqService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FaqService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateFaqAsync(CreateFaqCommand command)
        {
            await _unitOfWork.Faqs.CreateAsync( command.Question,command.Answer,command.PublishInMainPage,command.PublishInFrequentlyAskedQuestions,command.IsActive);

            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteFaqAsync(Guid id)
        {
            await _unitOfWork.Faqs.DeleteAsync(id);

            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<FaqDto>> GetAllFaqDtoAsync()
        {
            List<FAQ> faqs = await _unitOfWork.Faqs.GetAllsync();
            List<FaqDto> result = faqs.Select(f => new FaqDto
            {
                Id = f.Id,
                Question=f.Question,
                Answer=f.Answer,
                PublishInMainPage=f.PublishInMainPage,
                PublishInFrequentlyAskedQuestions=f.PublishInFrequentlyAskedQuestions,
                IsActive=f.IsActive

            }).ToList();
            return result;
        }

        public async Task<FaqDto?> GetByIdAsync(Guid id)
        {
            var faq = await _unitOfWork.Faqs.GetByIdAsync(id);
            if (faq == null) return null;

            return new FaqDto
            {
                Id = faq.Id,
                Question = faq.Question,
                Answer = faq.Answer,
                PublishInMainPage = faq.PublishInMainPage,
                PublishInFrequentlyAskedQuestions = faq.PublishInFrequentlyAskedQuestions,
                IsActive = faq.IsActive
            };
        }

        public async Task UpdateFaqAsync(UpdateFaqCommand command)
        {
            await _unitOfWork.Faqs.UpdateAsync( command.Id,command.Question,command.Answer,command.PublishInMainPage,command.PublishInFrequentlyAskedQuestions,command.IsActive);

            await _unitOfWork.CompleteAsync();
        }
    }
}