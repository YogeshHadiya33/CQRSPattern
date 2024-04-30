using CQRSPattern.Entity.User.Database;
using CQRSPattern.Repository.Context;
using CQRSPattern.Repository.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CQRSPattern.API.CustomInstaller;

public static class DatabaseInstaller
{
    public static void InstallDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CQRSContext>(options => { options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")); });

        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        //register identity user
        services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<CQRSContext>()
            .AddDefaultTokenProviders();
    }
}