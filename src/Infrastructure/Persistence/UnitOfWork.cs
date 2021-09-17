using System.Threading.Tasks;
using CleanArchitectureBase.Application.Contracts.Data;
using CleanArchitectureBase.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureBase.Infrastructure.Persistence
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
        }

        IUnitOfWorkScope IUnitOfWork.Begin() => new UnitOfWorkScope(this.context);
        IUnitOfWorkWriteScope IUnitOfWork.BeginWrite() => new UnitOfWorkWriteScope(this.context);

        private class UnitOfWorkScope : IUnitOfWorkScope
        {
            protected readonly ApplicationDbContext context;
   
            public IRepository<TEntity> EntitiesOf<TEntity>()
                where TEntity : EntityBase
            {
                return new Repository<TEntity>(context);
            }

            public UnitOfWorkScope(ApplicationDbContext context)
            {
                this.context = context;
                //this.context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }

            public virtual void Dispose() { }
        }

        private class UnitOfWorkWriteScope : UnitOfWorkScope, IUnitOfWorkWriteScope
        {
            public UnitOfWorkWriteScope(ApplicationDbContext context)
                : base(context)
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
            }

            async Task<int> IUnitOfWorkWriteScope.CompleteAsync() => await context.SaveChangesAsync();
        }
    }
}
