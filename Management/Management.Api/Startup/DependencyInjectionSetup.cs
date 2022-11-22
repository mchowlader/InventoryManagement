using Management.Common.Configuration;
using Management.Model.Data;
using Management.Services.Blogs;
using Management.Services.User;
using Management.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;

namespace Management.Api.Startup
{
    public static class DependencyInjectionSetup
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, 
            WebApplicationBuilder web)
        {
            ConfigurationManager Configuration = web.Configuration;

            services.AddDbContext<ApplicationDbContext>(option
                => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(Program).Assembly.FullName)));

            services.AddDbContext<BlogDbContext>(option
              => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
              b => b.MigrationsAssembly(typeof(Program).Assembly.FullName)));

            #region Custom Serveice Registration
            services.AddScoped<IAuthenticationServices, AuthenticationServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddConfiguaration<ConnectionStringConfig>(Configuration, "ConnectionStrings");
            services.AddIdentity<ApplicationUser, Role>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddEntityFrameworkStores<BlogDbContext>();
            services.Configure<MailConfiguration>(Configuration.GetSection("MailSettings"));


            services.AddScoped<IPostServices, PostServices>();

            #endregion

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
    public static class ConfigurationExtension
    {
        public static void AddConfiguaration<T>(this IServiceCollection services, IConfiguration configuration, string? configurationTag = null)
            where T : class
        {
            if(String.IsNullOrEmpty(configurationTag))
            {
                configurationTag = typeof(T).Name;
            }
            var instance = Activator.CreateInstance<T>();
            new ConfigureFromConfigurationOptions<T>(configuration.GetSection(configurationTag)).Configure(instance);
            services.AddSingleton(instance);
        }
    }
}