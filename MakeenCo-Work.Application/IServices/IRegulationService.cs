using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MakeenCo_Work.Application.Command;
using MakeenCo_Work.Application.DTOs;

namespace MakeenCo_Work.Application.IServices
{
    public interface IRegulationService
    {
        Task<List<RegulationDto>> GetAllRegulationDtoAsync();

        Task CreateRegulationAsync(CreateRegulationCommand command);

        Task UpdateRegulationAsync(UpdateRegulationCommand command);

        Task DeleteRegulationAsync(Guid id);
    }
}
