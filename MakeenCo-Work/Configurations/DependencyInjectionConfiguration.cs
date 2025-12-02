using MakeenCo_Work.Application.IServices;
using MakeenCo_Work.Application.Services;
using MakeenCo_Work.Domain.IRepository;
using MakeenCo_Work.Infrastructure.Repository;

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
        }
    }
}
