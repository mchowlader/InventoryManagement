using Management.Model.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Management.Api.Startup
{
    public static class DependencyInjectionSetup
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, 
            WebApplicationBuilder web/*, (string connectionString, string migrationAssemblyName) GetConnectionStringAndAssemblyName*/)
        {
            ConfigurationManager Configuration = web.Configuration;
            //var connectionInfo = GetConnectionStringAndAssemblyName;

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            //services.AddDbContext<ApplicationDbContext>(options
            //    => options.UseSqlServer(connectionInfo.connectionString, b =>
            //        b.MigrationsAssembly(connectionInfo.migrationAssemblyName)));

            services.AddDbContext<ApplicationDbContext>(option 
                => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), 
                b => b.MigrationsAssembly(typeof(Program).Assembly.FullName)));

            return services;
        }
    }
}