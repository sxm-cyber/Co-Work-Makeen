using Microsoft.EntityFrameworkCore;
using MakeenCo_Work.Application.Interfaces;
using MakeenCo_Work.Domain.Models;
using MakeenCo_Work.Infrastructure.Data;

namespace MakeenCo_Work.Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ApplicationDbContext _Context;

        public ReservationRepository(ApplicationDbContext context)
        {
            _Context = context;
        }
        public async Task<IEnumerable<Reservation>> GetAllAsync()=> await _Context.Reservations.ToListAsync();
        public async Task<Reservation?> GetByIdAsync(Guid id)=> await _Context.Reservations.FindAsync(id);
        public async Task AddAsync(Reservation reservation)=> await _Context.Reservations.AddAsync(reservation);
    }
}
