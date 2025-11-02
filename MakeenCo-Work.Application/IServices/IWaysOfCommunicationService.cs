using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MakeenCo_Work.Application.Command;
using MakeenCo_Work.Application.DTOs;
using MakeenCo_Work.Domain.Models;

namespace MakeenCo_Work.Application.IServices
{
    public interface IWaysOfCommunicationService
    {
        Task<List<WaysOfCommunicationDto>> GetAllWaysOfCommunicationAsync();
        Task CreateWaysOfCommunicationAsync(CreateWaysOfCommunicationCommand command);
        Task UpdateWaysOfCommunicationAsync(UpdateWaysOfCommunicationCommand command);

    }
}
