using MakeenCo_Work.Domain.IRepository;
using MakeenCo_Work.Domain.Repositories;

namespace MakeenCo_Work.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }

        IFaqRepository Faqs {get;}

        IRegulationRepository Regulations { get; }

        IWaysOfCommunicationRepository WaysOfCommunications { get; }

        IDiscountCodeRepository DiscountCodes { get; }

        ITieredDiscountRepository TieredDiscounts { get; }

        IMessageRepository Messages { get; }

        IReservationRepository Reservations { get; }

        ISpaceRepository Spaces { get; }

        IBlogPostRepository BlogPosts { get; }

        Task<int> CompleteAsync();
    }
}
