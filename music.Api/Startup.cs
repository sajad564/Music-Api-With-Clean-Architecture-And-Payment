using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore ; 
using music.Infrastructure.Common;
using music.Infrastructure.Data;
using music.Api.Filters;
using music.Services;

namespace music.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => {
                options.Filters.Add<ValidationFilter>() ; 
            })
            .AddFluentValidation(conf => conf.RegisterValidatorsFromAssemblyContaining<Startup>())
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(opt=>{
            opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddApi(Configuration) ; 
            services.AddData(Configuration) ;
            services.AddServices(Configuration) ; 
            services.AddCommon(Configuration) ;  
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage() ; 
                app.UseExceptionHandler("/api/error") ;
            }
            else
            {
                 
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseStaticFiles() ; 
            app.UseCors("mycorsPolicy") ; 
            app.UseSwagger() ; 
            app.UseSwaggerUI(options =>  {
                options.SwaggerEndpoint("/swagger/v1/swagger.json" ,"api end point consuming v1" ) ; 
            }) ; 

            app.UseMvc();
        }
    }
}
