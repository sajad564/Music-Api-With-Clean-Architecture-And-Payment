using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using music.Domain;
using music.Domain.Entities;
using music.Infrastructure.Data.Data;

namespace music.Infrastructure.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddData(this IServiceCollection services , IConfiguration configuration)
         {
            services.AddDbContextPool<MusicDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Music"))) ; 
            services.AddTransient<IUnitOfWork , UnitOfWork>() ;
            services.AddIdentity<User,IdentityRole>(options => {
                options.Password.RequireDigit = false ;
                options.Password.RequiredLength = 5 ;
                options.Password.RequireDigit =false ; 
                options.Password.RequireLowercase = false ;
                options.Password.RequireUppercase  =false ;
                options.Password.RequireNonAlphanumeric = false ; 
            })
            .AddEntityFrameworkStores<MusicDbContext>() 
            .AddDefaultTokenProviders() ; 
            return services ;
         }
    }
}