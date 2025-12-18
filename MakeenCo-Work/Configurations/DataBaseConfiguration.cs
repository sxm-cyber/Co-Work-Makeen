using MakeenCo_Work.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MakeenCo_Work.Configurations;

public static class DataBaseConfiguration
{
    public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
    }
}