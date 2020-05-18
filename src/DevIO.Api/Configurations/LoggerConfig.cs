using System;
using DevIO.Api.Extensions;
using Elmah.Io.AspNetCore;
using Elmah.Io.AspNetCore.HealthChecks;
using HealthChecks.UI.Client;
using HealthChecks.UI.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevIO.Api.Configurations
{
    public static class LoggerConfig
    {
        public static IServiceCollection AddLoggingConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddElmahIo(o =>
            {
                o.ApiKey = "8efc7e26207b4971badfc70a0e4304cb";
                o.LogId = new Guid("9a9f7dc9-61a7-4296-9db6-00ac120dba2c");
            });

            services.AddHealthChecks()
                .AddElmahIoPublisher(options =>
                {
                    options.ApiKey = "8efc7e26207b4971badfc70a0e4304cb";
                    options.LogId = new Guid("9a9f7dc9-61a7-4296-9db6-00ac120dba2c");
                    options.HeartbeatId = "API Fornecedores";

                })
                .AddCheck("Produtos", new SqlServerHealthCheck(configuration.GetConnectionString("DefaultConnection")))
                .AddSqlServer(configuration.GetConnectionString("DefaultConnection"), name: "BancoSQL");

            services.AddHealthChecksUI();

            return services;
        }

        public static IApplicationBuilder UseLoggingConfiguration(this IApplicationBuilder app)
        {
            app.UseElmahIo();

            app.UseHealthChecks("/api/hc", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            app.UseHealthChecksUI(options => { options.UIPath = "/api/ui-hc"; });

            return app;
        }
    }
}