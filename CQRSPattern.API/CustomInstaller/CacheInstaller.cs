namespace CQRSPattern.API.CustomInstaller;

public static class CacheInstaller
{
    public static void ConfigureCaching(this IServiceCollection services, IConfiguration configuration)
    {
        //In Memory Cache
        services.AddMemoryCache();

        //Redis Cache
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
            options.InstanceName = "CQRSPattern_";
        });
    }
}