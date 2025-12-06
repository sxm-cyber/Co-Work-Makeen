using MakeenCo_Work.Domain.IRepository;
using MakeenCo_Work.Domain.Repositories;
using System.Threading.Tasks;

namespace MakeenCo_Work.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IReservationRepository Reservations { get; }
        ISpaceRepository Spaces { get; }
        IBlogPostRepository BlogPosts { get; }
        Task<int> CompleteAsync();
    }
}
