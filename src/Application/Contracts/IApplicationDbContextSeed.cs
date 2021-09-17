using System.Threading.Tasks;

namespace CleanArchitectureBase.Application.Contracts
{
    public interface IApplicationDbContextSeed
    {
        Task SeedDefaultUserAsync();

        Task SeedSampleDataAsync();
    }
}
