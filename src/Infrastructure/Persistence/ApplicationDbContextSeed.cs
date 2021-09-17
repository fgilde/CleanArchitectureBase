using CleanArchitectureBase.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitectureBase.Application.Contracts;

namespace CleanArchitectureBase.Infrastructure.Persistence
{
    public class ApplicationDbContextSeed: IApplicationDbContextSeed
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IApplicationDbContext _dbContext;

        public ApplicationDbContextSeed(UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, IApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }

        public async Task SeedDefaultUserAsync()
        {
            var administratorRole = new IdentityRole("Administrator");

            if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await _roleManager.CreateAsync(administratorRole);
            }

            var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

            if (_userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await _userManager.CreateAsync(administrator, "Administrator1!");
                await _userManager.AddToRolesAsync(administrator, new [] { administratorRole.Name });
            }
        }

        public async Task SeedSampleDataAsync()
        {
            // Seed, if necessary
            //if (!_dbContext.EntitiesOf<MyEntity>().Any())
            //{
            //    // Add
            //    await context.SaveChangesAsync();
            //}
        }
    }
}
