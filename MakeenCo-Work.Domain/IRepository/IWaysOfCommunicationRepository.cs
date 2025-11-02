using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MakeenCo_Work.Domain.Models;

namespace MakeenCo_Work.Domain.IRepository
{
    public interface IWaysOfCommunicationRepository
    {
        Task CreateAsync(
            string address, string phoneNumber, string landlineNumber,
            string baleLink, string instagramLink, string linkdinLink, string makeenWebsiteLink);
        Task<List<WaysOfCommunication>> GetAllAsync();
        Task<WaysOfCommunication?> GetByIdAsync(Guid id);
        Task UpdateAsync(
           Guid Id, string address, string phoneNumber, string landlineNumber,
            string baleLink, string instagramLink, string linkdinLink, string makeenWebsiteLink);
    }
}
