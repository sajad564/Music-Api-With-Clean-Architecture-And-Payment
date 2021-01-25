using System.Reflection.Metadata.Ecma335;
using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using music.Api.Common;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using music.Infrastructure.Common.Common;

namespace music.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApi(this IServiceCollection services , IConfiguration config)
        {
            var authConf = config.GetSection("AuthConfig").Get<AuthConfigurations>() ; 
            services.AddSingleton(authConf) ; 
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddAutoMapper(typeof(Startup));
            services.AddCors(options => {
                options.AddPolicy(name : "mycorsPolicy" , builder => {
                    builder.WithOrigins(config.GetSection("ValidOrigin").Value)
                    .AllowAnyMethod()
                    .AllowAnyHeader() ;  
                });
            });
            services.ConfigureApplicationCookie(options => {
                    options.Events.OnRedirectToLogin = context => {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
                }) ; 
            services.AddAuthentication(options => { 
                options.DefaultAuthenticateScheme = "MYSCHEME" ;
            })
                .AddJwtBearer("MYSCHEME", options =>{
                    options.RequireHttpsMetadata = false ;
                    options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuer = true  ,
                                ValidateAudience = false  , 
                                ValidateIssuerSigningKey = true ,
                                ValidateLifetime = true ,  
                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config.GetSection("AuthConfig:Key").Value)) ,
                                ValidIssuer = config.GetSection("AuthConfig:Issuer").Value , 
                            } ; 
                }) ;
            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1" , new Microsoft.OpenApi.Models.OpenApiInfo{ Version = "v1" , Title = "end points consuming" }) ; 
            }) ;
            return services ; 
        }
    }
}