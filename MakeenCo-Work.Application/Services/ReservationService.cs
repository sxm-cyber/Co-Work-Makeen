using MakeenCo_Work.Domain.Models;
using MakeenCo_Work.Application.Commands;
using MakeenCo_Work.Application.DTOs;
using MakeenCo_Work.Application.Interfaces;

namespace MakeenCo_Work.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWork _Unit;

        public ReservationService(IUnitOfWork unit)
        {
            _Unit = unit;
        }

        public async Task<IEnumerable<ReservationDto>> GetAllAsync()
        {
            var reservations = await _Unit.Reservations.GetAllAsync();
            return reservations.Select(x => new ReservationDto
            {
                Id = x.Id,
                ReservationNumber = x.ReservationNumber,
                Status = x.Status.ToString(),
                TotalAmount = x.TotalAmount
            });
        }

        public async Task<ReservationDto?> CreateAsync(CreateReservationCommand command)
        {
            var reservation = new Reservation( Guid.NewGuid().ToString().Substring(0, 8),
                command.UserId,
                command.SpaceId,
                command.StartDate,
                command.EndDate,
                command.StartTime,
                command.EndTime,
                command.TotalAmount,
                command.Notes,
                command.DiscountCodeId
            );

            await _Unit.Reservations.AddAsync(reservation);
            await _Unit.CompleteAsync();

            return new ReservationDto
            {
                Id = reservation.Id,
                ReservationNumber = reservation.ReservationNumber,
                Status = reservation.Status.ToString(),
                TotalAmount = reservation.TotalAmount
            };
        }

        public async Task<ReservationDto?> ConfirmAsync(Guid id)
        {
            var reserv = await _Unit.Reservations.GetByIdAsync(id);
            if (reserv == null) return null;

            reserv.Confirm();
            await _Unit.CompleteAsync();

            return new ReservationDto
            {
                Id = reserv.Id,
                ReservationNumber = reserv.ReservationNumber,
                Status = reserv.Status.ToString(),
                TotalAmount = reserv.TotalAmount
            };
        }

        public async Task<ReservationDto?> CancelAsync(Guid id, CancelCommand command)
        {
            var reserv = await _Unit.Reservations.GetByIdAsync(id);
            if (reserv == null) return null;

            reserv.Cancel(command.Reason);
            await _Unit.CompleteAsync();

            return new ReservationDto
            {
                Id = reserv.Id,
                ReservationNumber = reserv.ReservationNumber,
                Status = reserv.Status.ToString(),
                TotalAmount = reserv.TotalAmount
            };
        }
    }
}
