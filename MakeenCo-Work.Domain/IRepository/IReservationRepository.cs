using MakeenCo_Work.Domain.Models;

namespace MakeenCo_Work.Application.Interfaces
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetAllAsync();
        Task<Reservation?> GetByIdAsync(Guid id);
        Task AddAsync(Reservation reservation);
    }
}
