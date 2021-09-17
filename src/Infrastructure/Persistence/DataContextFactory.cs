using System.IO;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureBase.Infrastructure.Persistence
{
    public class DataContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            IServiceCollection services = new ServiceCollection();

            return services.Configure<OperationalStoreOptions>(x => { })
                .AddConnection(configuration)
                .BuildServiceProvider().GetService<ApplicationDbContext>();
        }
    }
}
