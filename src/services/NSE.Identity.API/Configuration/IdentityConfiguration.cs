using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NSE.Identity.API.Data;
using NSE.WebAPI.Core.Identity;

namespace NSE.Identity.API.Configuration;

public static class IdentityConfiguration
{
    public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddDefaultIdentity<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        
        services.AddAuthConfiguration(configuration);
        
        return services;
    }
}