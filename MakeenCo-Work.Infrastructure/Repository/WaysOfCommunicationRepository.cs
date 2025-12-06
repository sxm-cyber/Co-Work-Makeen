using MakeenCo_Work.Domain.IRepository;
using MakeenCo_Work.Domain.Models;
using MakeenCo_Work.Infrastructure.Data;

namespace MakeenCo_Work.Infrastructure.Repository
{
    public class WaysOfCommunicationRepository : BaseRepository<WaysOfCommunication> , IWaysOfCommunicationRepository
    {

        public WaysOfCommunicationRepository(ApplicationDbContext context) : base(context) { }

        
        public async Task CreateAsync(
            string address,
            string phoneNumber,
            string landlineNumber,
            string baleLink,
            string instagramLink,
            string linkdinLink,
            string makeenWebsiteLink)
        {
            var waysOfCommunication = new WaysOfCommunication(address, phoneNumber, landlineNumber, baleLink, instagramLink, linkdinLink, makeenWebsiteLink);

            await CreateAsync(waysOfCommunication);
        }


        public async Task<List<WaysOfCommunication>> GetAllAsync()
        {
            var waysOfCommunication = await GetAllAsync(1, int.MaxValue);

            return waysOfCommunication.ToList();
        }


        public async Task UpdateAsync(Guid Id, string address, string phoneNumber, string landlineNumber, string baleLink, string instagramLink, string linkdinLink, string makeenWebsiteLink)
        {
            var waysOfCommunication = await GetByIdAsync(Id);

            if (waysOfCommunication is null)
                return;

            waysOfCommunication.Update(address, phoneNumber, landlineNumber, baleLink, instagramLink, linkdinLink, makeenWebsiteLink);

            await UpdateAsync(waysOfCommunication);
        }
    }
}