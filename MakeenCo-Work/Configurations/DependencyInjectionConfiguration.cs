using MakeenCo_Work.Application.Interfaces;
using MakeenCo_Work.Application.IServices;
using MakeenCo_Work.Application.Services;
using MakeenCo_Work.Domain.IRepository;
using MakeenCo_Work.Domain.Repositories;
using MakeenCo_Work.Infrastructure.Repositories;
using MakeenCo_Work.Infrastructure.Repository;
using MakeenCo_Work.Infrastructure.UnitOfWork;

namespace MakeenCo_Work.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependency(this IServiceCollection service)
        {
            //Repositories
            service.AddScoped<IWaysOfCommunicationRepository, WaysOfCommunicationRepository>();
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IFaqRepository,FaqRepository>();
            service.AddScoped<IRegulationRepository, RegulationRepository>();
            service.AddScoped<IMessageRepository, MessageRepository>();
            service.AddScoped<ITieredDiscountRepository, TieredDiscountRepository>();
            service.AddScoped<IDiscountCodeRepository, DiscountCodeRepository>();
            service.AddScoped<IReservationRepository, ReservationRepository>();
            service.AddScoped<ISpaceRepository, SpaceRepository>();
            service.AddScoped<IBlogPostRepository, BlogPostRepository>();


            // Unit Of Work
            service.AddScoped<IUnitOfWork, UnitOfWork>();



            //Services
            service.AddScoped<IWaysOfCommunicationService, WaysOfCommunicationService>();
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IAuthService, AuthService>();
            service.AddScoped<IOtpService, OtpService>();
            service.AddScoped<IFileStorageService, FileStorageService>();
            service.AddScoped<IBulkUserService, BulkUserService>();
            service.AddScoped<IFaqService, FaqService>();
            service.AddScoped<IRegulationService, RegulationService>();
            service.AddScoped<IMessageService, MessageService>();
            service.AddScoped<IDiscountCodeService, DiscountCodeService>();
            service.AddScoped<ITieredDiscountService, TieredDiscountService>();
            service.AddScoped<IBlogPostService, BlogPostService>();
            service.AddScoped<IReservationService, ReservationService>();
            service.AddScoped<ISpaceService, SpaceService>();
        }
    }
}
