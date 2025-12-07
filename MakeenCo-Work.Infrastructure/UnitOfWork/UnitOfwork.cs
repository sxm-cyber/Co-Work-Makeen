using MakeenCo_Work.Application.Interfaces;
using MakeenCo_Work.Domain.IRepository;
using MakeenCo_Work.Domain.Repositories;
using MakeenCo_Work.Infrastructure.Data;
using MakeenCo_Work.Infrastructure.Repositories;
using MakeenCo_Work.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;

namespace MakeenCo_Work.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _Context;


        public IUserRepository Users { get; }


        public IFaqRepository Faqs { get; }

        public IRegulationRepository Regulations { get; }

        public IWaysOfCommunicationRepository WaysOfCommunications { get; }

        public IDiscountCodeRepository DiscountCodes { get; }

        public ITieredDiscountRepository TieredDiscounts { get; }

        public IMessageRepository Messages { get; }

        public IReservationRepository Reservations { get; }

        public ISpaceRepository Spaces { get; }

        public IBlogPostRepository BlogPosts { get; }

        public UnitOfWork(ApplicationDbContext context , IConfiguration configuration)
        {
            _Context = context;


            Users = new UserRepository(context, null, configuration);

            Faqs = new FaqRepository(context);

            Regulations = new RegulationRepository(context);

            WaysOfCommunications = new WaysOfCommunicationRepository(context);

            DiscountCodes = new DiscountCodeRepository(context, configuration);

            TieredDiscounts = new TieredDiscountRepository(context , configuration);

            Messages = new MessageRepository(context, configuration);

            Reservations = new ReservationRepository(context);

            Spaces = new SpaceRepository(context);

            BlogPosts =new BlogPostRepository(context);
        }


        public async Task<int> CompleteAsync()
        {
            return await _Context.SaveChangesAsync();
            
        }
    }
}
