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




            //Services
            service.AddScoped<IWaysOfCommunicationService, WaysOfCommunicationService>();
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IAuthService, AuthService>();
            service.AddScoped<IOtpService, OtpService>();
            service.AddScoped<IFileStorageService, FileStorageService>();
            service.AddScoped<IBulkUserService, BulkUserService>();
        }
    }
}
