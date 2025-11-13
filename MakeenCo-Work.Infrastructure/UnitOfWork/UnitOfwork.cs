using MakeenCo_Work.Application.Interfaces;
using MakeenCo_Work.Domain.IRepository;
using MakeenCo_Work.Infrastructure.Data;
using MakeenCo_Work.Infrastructure.Repositories;
using MakeenCo_Work.Infrastructure.Repository;

namespace MakeenCo_Work.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _Context;
        public IReservationRepository Reservations { get; }
        public ISpaceRepository Spaces { get; }

 

        public UnitOfWork(ApplicationDbContext context)
        {
            _Context = context;
            Reservations = new ReservationRepository(context);
            Spaces = new SpaceRepository(context);

        }

 

        public async Task<int> CompleteAsync()
        {
            return await _Context.SaveChangesAsync();
            
        }
    }
}
