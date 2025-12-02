//using MakeenCo_Work.Application.Interfaces;
//using MakeenCo_Work.Application.IServices;
//using MakeenCo_Work.Application.Services;
//using MakeenCo_Work.Domain.IRepository;
//using MakeenCo_Work.Infrastructure.Repositories;
//using MakeenCo_Work.Infrastructure.Repository;
//using MakeenCo_Work.Infrastructure.UnitOfWork;
//using Microsoft.Extensions.DependencyInjection;

//namespace MakeenCo_Work.Configurations
//{
//    public static class DependencyInjectionConfiguration
//    {
//        public static void AddDependency(this IServiceCollection service)
//        {
//            service.AddScoped<IWaysOfCommunicationRepository, WaysOfCommunicationRepository>();
//            service.AddScoped<IWaysOfCommunicationService, WaysOfCommunicationService>();
            
//            service.AddScoped<ISpaceRepository,SpaceRepository>();
//            service.AddScoped<ISpaceService,SpaceService>();

//            service.AddScoped<IReservationRepository, ReservationRepository>();
//            service.AddScoped<IReservationService, ReservationService>();
//            service.AddScoped<IUnitOfWork, UnitOfWork>();
//        }
//    }
//}
