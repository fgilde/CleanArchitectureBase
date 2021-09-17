using System.Threading;
using System.Threading.Tasks;
using CleanArchitectureBase.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CleanArchitectureBase.Application.Contracts
{
    public interface IApplicationDbContext
    {
        DatabaseFacade Database { get; }
        DbSet<TEntity> EntitiesOf<TEntity>()
            where TEntity : EntityBase;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
