using MakeenCo_Work.Domain.Models;

namespace MakeenCo_Work.Domain.IRepository
{
    public interface IWaysOfCommunicationRepository : IBaseRepository<WaysOfCommunication>
    {
        Task CreateAsync(
            string address, string phoneNumber, string landlineNumber,
            string baleLink, string instagramLink, string linkdinLink, string makeenWebsiteLink);

        Task<List<WaysOfCommunication>> GetAllAsync();

        Task UpdateAsync(
           Guid Id, string address, string phoneNumber, string landlineNumber,
            string baleLink, string instagramLink, string linkdinLink, string makeenWebsiteLink);
    }
}