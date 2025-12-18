namespace MakeenCo_Work.Configurations;

public static class CacheConfiguration
{
    public static void AddCacheConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        // Redis Cache
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
            options.InstanceName = "MakeenCoWork:";
        });
        
        // MemoryCache
        services.AddMemoryCache();
    }
}