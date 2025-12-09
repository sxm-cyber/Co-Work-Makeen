using MakeenCo_Work.Application.Command;
using MakeenCo_Work.Application.DTOs;
using MakeenCo_Work.Application.Interfaces;
using MakeenCo_Work.Application.IServices;
using MakeenCo_Work.Domain.Models;

namespace MakeenCo_Work.Application.Services
{
    public class RegulationService : IRegulationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegulationService(IUnitOfWork  unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateRegulationAsync(CreateRegulationCommand command)
        {
            await _unitOfWork.Regulations.CreateAsync(command.Title, command.Content, command.IsActive);

            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteRegulationAsync(Guid id)
        {
            await _unitOfWork.Regulations.DeleteAsync(id);

            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<RegulationDto>> GetAllRegulationDtoAsync()
        {
            List<Regulation> regul = await _unitOfWork.Regulations.GetAllAsync();
            List<RegulationDto> result = regul.Select(r => new RegulationDto
            {
                Id = r.Id,
                Title=r.Title,
                Content=r.Content,
                IsActive = r.IsActive

            }).ToList();
            return result;
        }

        public async Task<RegulationDto?> GetById(Guid Id)
        {
            var regul = await _unitOfWork.Regulations.GetByIdAsync(Id);
            if (regul == null) return null;
            return new RegulationDto
            {
                Id = regul.Id,
                Title = regul.Title,
                Content = regul.Content,
                IsActive = regul.IsActive


            };
        }

        public async Task UpdateRegulationAsync(UpdateRegulationCommand command)
        {
            await _unitOfWork.Regulations.UpdateAsync(command.Id,command.Title,command.Content,command.IsActive);

            await _unitOfWork.CompleteAsync();
        }
    }
}