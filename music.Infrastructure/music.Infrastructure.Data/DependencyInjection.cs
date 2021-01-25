using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using music.Domain;
using music.Infrastructure.Data.Data;

namespace music.Infrastructure.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddData(this IServiceCollection services , IConfiguration configuration)
         {
            services.AddDbContextPool<MusicDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Music"))) ; 
            services.AddTransient<IUnitOfWork , UnitOfWork>() ;
            return services ;
         }
    }
}