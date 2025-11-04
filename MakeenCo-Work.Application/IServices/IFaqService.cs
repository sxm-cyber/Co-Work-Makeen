using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MakeenCo_Work.Application.Command;
using MakeenCo_Work.Application.DTOs;

namespace MakeenCo_Work.Application.IServices
{
    public interface IFaqService
    {
        Task<List<FaqDto>> GetAllFaqDtoAsync();

        Task CreateFaqAsync(CreateFaqCommand command);

        Task UpdateFaqAsync(UpdateFaqCommand command);

        Task DeleteFaqAsync(Guid id);
    }
}
