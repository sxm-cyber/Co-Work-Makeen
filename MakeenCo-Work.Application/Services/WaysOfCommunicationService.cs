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

namespace MakeenCo_Work.Application.Services
{
    public class WaysOfCommunicationService : IWaysOfCommunicationService
    {
        private readonly IWaysOfCommunicationRepository
            _waysOfCommunicationRepository;
        public WaysOfCommunicationService(
            IWaysOfCommunicationRepository  waysOfCommunicationRepository)
        {
             _waysOfCommunicationRepository= waysOfCommunicationRepository;
            
        }

        public async Task CreateWaysOfCommunicationAsync(
            CreateWaysOfCommunicationCommand command)
        {
            await _waysOfCommunicationRepository.CreateAsync(
                command.Address, command.PhoneNumber, command.LandlineNumber
                , command.BaleLink, command.InstagramLink, command.LinkdinLink, command.MakeenWebsiteLink);

        }

        public async Task<List<WaysOfCommunicationDto>> GetAllWaysOfCommunicationAsync()
        {
            List<WaysOfCommunication> WaysOfCommunication=await _waysOfCommunicationRepository.GetAllAsync();
            List<WaysOfCommunicationDto> WaysOfCommunicationResalt= WaysOfCommunication.Select(w=>new WaysOfCommunicationDto
            {
                Address=w.Address,
                PhoneNumber=w.PhoneNumber,
                LandlineNumber=w.LandlineNumber,
                BaleLink=w.BaleLink,
                InstagramLink=w.InstagramLink,
                LinkdinLink=w.LinkdinLink,
                MakeenWebsiteLink=w.MakeenWebsiteLink
            }).ToList();
            return WaysOfCommunicationResalt;
        }

        public async Task UpdateWaysOfCommunicationAsync(UpdateWaysOfCommunicationCommand command)
        {
            await _waysOfCommunicationRepository.UpdateAsync(
                command.Id, command.Address, command.PhoneNumber, command.LandlineNumber
                , command.BaleLink, command.InstagramLink, command.LinkdinLink, command.MakeenWebsiteLink);
        }
    }
}
