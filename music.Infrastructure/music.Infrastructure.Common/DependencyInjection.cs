
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using music.Domain.Services;
using music.Infrastructure.Common.Common;
using music.Infrastructure.Common.Services;

namespace music.Infrastructure.Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCommon(this IServiceCollection services , IConfiguration Configuration)
        {
            var pathConf = Configuration.GetSection("PathConfig").Get<PathConfiguration>() ; 
            var authConf = Configuration.GetSection("AuthConfig").Get<AuthConfigurations>() ; 
            var emailConf = Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>() ; 
            var confirmRecoveryConf = Configuration.GetSection("ConfirmAndRecovery").Get<ConfirmAndRecovery>() ; 
            services.AddSingleton(pathConf) ;
            services.AddSingleton(authConf) ;
            services.AddSingleton(emailConf) ;
            services.AddSingleton(confirmRecoveryConf) ;  
            services.AddTransient<ITokenGenerator , TokenGenerator>() ;   
            services.AddTransient<IFileManager , FileManager>() ; 
            services.AddTransient<IEmailSender,EmailSender>() ; 
            return services ; 
        }
    }
}