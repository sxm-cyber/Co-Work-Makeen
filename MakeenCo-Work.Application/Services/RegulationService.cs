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
    public class RegulationService : IRegulationService
    {
        private readonly IRegulationRepository _regulationRepository;

        public RegulationService(IRegulationRepository  regulationRepository)
        {
            _regulationRepository = regulationRepository;
        }

        public async Task CreateRegulationAsync(CreateRegulationCommand command)
        {
            await _regulationRepository.CreateAsync(command.Title, command.Content, command.IsActive);
        }

        public async Task DeleteRegulationAsync(Guid id)
        {
            await _regulationRepository.DeleteAsync(id);
        }

        public async Task<List<RegulationDto>> GetAllRegulationDtoAsync()
        {
            List<Regulation> regul = await _regulationRepository.GetAllAsync();
            List<RegulationDto> result = regul.Select(r => new RegulationDto
            {
                Id = r.Id,
                Title=r.Title,
                Content=r.Content,
                IsActive = r.IsActive

            }).ToList();
            return result;
        }

        public async Task UpdateRegulationAsync(UpdateRegulationCommand command)
        {
            await _regulationRepository.UpdateAsync(command.Id,command.Title,command.Content,command.IsActive);
        }
    }
}
