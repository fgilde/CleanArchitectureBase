using CleanArchitectureBase.Application;
using CleanArchitectureBase.Application.Contracts;
using CleanArchitectureBase.Application.Contracts.Data;
using CleanArchitectureBase.Infrastructure.Files;
using CleanArchitectureBase.Infrastructure.Identity;
using CleanArchitectureBase.Infrastructure.Persistence;
using CleanArchitectureBase.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureBase.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddConnection(configuration);

            services.AddTransient<IApplicationDbContextSeed, ApplicationDbContextSeed>();
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddScoped<IDomainEventService, DomainEventService>();

            services
                .AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Constants.Policies.CanPurge, policy => policy.RequireRole(Constants.Roles.Administrator));
            });
            return services;
        }

        internal static IServiceCollection AddConnection(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionName = configuration.GetValue<string>("ConnectionToUse");
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString(connectionName);
                if (configuration.GetValue<bool>("UseInMemoryDatabase"))
                    options.UseInMemoryDatabase("CleanArchitectureBaseDb");
                else if (IsSqlite(connectionString))
                    options.UseSqlite(connectionString);
                else
                    options.UseSqlServer(connectionString, b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            });

            return services;
        }

        private static bool IsSqlite(string connectionString)
        {
            var lower = connectionString.ToLower();
            return lower.Contains(".db") && !lower.Contains("server=") && lower.Contains("data source=");
        }
    }
}
