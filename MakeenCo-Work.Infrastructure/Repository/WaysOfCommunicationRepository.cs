using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MakeenCo_Work.Domain.IRepository;
using MakeenCo_Work.Domain.Models;
using MakeenCo_Work.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MakeenCo_Work.Infrastructure.Repository
{
    public class WaysOfCommunicationRepository : IWaysOfCommunicationRepository
    {
        private ApplicationDbContext _context;
        public WaysOfCommunicationRepository(ApplicationDbContext context)
        {
            _context = context;
            
        }
        public async Task CreateAsync(string address, string phoneNumber, string landlineNumber, string baleLink, string instagramLink, string linkdinLink, string makeenWebsiteLink)
        {
           var waysOfCommunication= new WaysOfCommunication(address,phoneNumber,landlineNumber,  baleLink,  instagramLink,  linkdinLink,  makeenWebsiteLink);
            await _context.WaysOfCommunications.AddAsync(waysOfCommunication);
            await _context.SaveChangesAsync();
        }

        public async Task<List<WaysOfCommunication>> GetAllAsync()
        {
            var waysOfCommunication=await _context.WaysOfCommunications.ToListAsync();
            return waysOfCommunication;
        }

        public async Task<WaysOfCommunication?> GetByIdAsync(Guid id)
        {
            var waysOfCommunication = await _context.WaysOfCommunications.FindAsync(id);
            return waysOfCommunication;


        }

        public async Task UpdateAsync(Guid Id, string address, string phoneNumber, string landlineNumber, string baleLink, string instagramLink, string linkdinLink, string makeenWebsiteLink)
        {
            var waysOfCommunication=await GetByIdAsync(Id);
            if (waysOfCommunication != null)
            {
                waysOfCommunication.Update(address, phoneNumber, landlineNumber, baleLink, instagramLink, linkdinLink, makeenWebsiteLink);
                await _context.SaveChangesAsync();
            }
        }
    }
}
