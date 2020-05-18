using AutoMapper;
using DevIO.Api.Configurations;
using DevIO.Api.Extensions;
using DevIO.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevIO.Api
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
            services.AddDbContext<MeuDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentityConfig(Configuration);

            services.AddAutoMapper(typeof(Startup));

            services.WebApiConfig();

            services.AddSwaggerConfig();

            services.AddLoggingConfig(Configuration);

            services.ResolveDependencies();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseCors("Development");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseCors("Development"); // Usar apenas nas demos => Configuração Ideal: Production
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMvcConfiguration();
            app.UseSwaggerConfig(provider);
            app.UseLoggingConfiguration();
        }
    }
}
