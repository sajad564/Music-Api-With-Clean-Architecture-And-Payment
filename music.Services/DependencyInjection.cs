using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using music.Domain.Common;
using music.Domain.Services.Internal;
using music.Services.Common;
using music.Services.services;

namespace music.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services , IConfiguration configuration)
        {
            var paymentConf = configuration.GetSection("PaymentConfig").Get<PaymentConfig>() ;
            services.AddSingleton(paymentConf) ;
            services.AddTransient<IShopingService,ShopingService>() ;
            services.AddTransient<IPhotoService,PhotoService>() ; 
            services.AddTransient<IFileService ,FileService>() ; 
            services.AddTransient<IUserService,UserService>() ; 
            services.AddSingleton<IErrorMessages,ErrorMessages>() ;
            services.AddTransient<IMusicService ,MusicService>() ;
            services.AddTransient<IAlbumService ,AlbumService>() ; 
            services.AddTransient<IGradeService , GradeService>() ; 
            services.AddTransient<ICartService,CartService>() ; 
            return services ; 
        }
    }
}