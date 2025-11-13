using MakeenCo_Work.Application.Commands;
using MakeenCo_Work.Application.DTOs;

namespace MakeenCo_Work.Application.Services
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationDto>> GetAllAsync();
        Task<ReservationDto?> CreateAsync(CreateReservationCommand command);
        Task<ReservationDto?> ConfirmAsync(Guid id);
        Task<ReservationDto?> CancelAsync(Guid id, CancelCommand command);
    }
}
