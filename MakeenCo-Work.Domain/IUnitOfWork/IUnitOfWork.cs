using MakeenCo_Work.Domain.IRepository;
using System.Threading.Tasks;

namespace MakeenCo_Work.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IReservationRepository Reservations { get; }
        ISpaceRepository Spaces { get; }
        Task<int> CompleteAsync();
    }
}
