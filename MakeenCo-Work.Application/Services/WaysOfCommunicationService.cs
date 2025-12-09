using MakeenCo_Work.Application.Command;
using MakeenCo_Work.Application.DTOs;
using MakeenCo_Work.Application.Interfaces;
using MakeenCo_Work.Application.IServices;
using MakeenCo_Work.Domain.Models;

namespace MakeenCo_Work.Application.Services
{
    public class WaysOfCommunicationService : IWaysOfCommunicationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public WaysOfCommunicationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }

        public async Task CreateWaysOfCommunicationAsync(CreateWaysOfCommunicationCommand command)
        {
            await _unitOfWork.WaysOfCommunications.CreateAsync(
                command.Address, command.PhoneNumber, command.LandlineNumber
                , command.BaleLink, command.InstagramLink, command.LinkdinLink, command.MakeenWebsiteLink);

            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<WaysOfCommunicationDto>> GetAllWaysOfCommunicationAsync()
        {
            List<WaysOfCommunication> WaysOfCommunication=await _unitOfWork.WaysOfCommunications.GetAllAsync();
            List<WaysOfCommunicationDto> WaysOfCommunicationResalt= WaysOfCommunication.Select(w=>new WaysOfCommunicationDto
            {
                Id=w.Id,
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

        public async Task<WaysOfCommunicationDto?> GetByIdAsync(Guid id)
        {
            var weyseof = await _unitOfWork.WaysOfCommunications.GetByIdAsync(id);

            if (weyseof == null)
            {
                return null;
                
            }
            return new WaysOfCommunicationDto
            {
                Id = weyseof.Id,
                Address = weyseof.Address,
                PhoneNumber = weyseof.PhoneNumber,
                LandlineNumber = weyseof.LandlineNumber,
                BaleLink = weyseof.BaleLink,
                InstagramLink = weyseof.InstagramLink,
                LinkdinLink = weyseof.LinkdinLink,
                MakeenWebsiteLink = weyseof.MakeenWebsiteLink

            };
        }

        public async Task UpdateWaysOfCommunicationAsync(UpdateWaysOfCommunicationCommand command)
        {
            await _unitOfWork.WaysOfCommunications.UpdateAsync(
                command.Id, command.Address, command.PhoneNumber, command.LandlineNumber
                , command.BaleLink, command.InstagramLink, command.LinkdinLink, command.MakeenWebsiteLink);

            await _unitOfWork.CompleteAsync();
        }
    }
}