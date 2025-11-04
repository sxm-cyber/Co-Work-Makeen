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
            service.AddScoped<IWaysOfCommunicationRepository, WaysOfCommunicationRepository>();
            service.AddScoped<IWaysOfCommunicationService, WaysOfCommunicationService>();
            service.AddScoped<IFaqRepository,FaqRepository>();
            service.AddScoped<IFaqService,FaqService>();

        }
    }
}
