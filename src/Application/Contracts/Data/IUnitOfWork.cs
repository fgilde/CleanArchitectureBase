using System;
using System.Threading.Tasks;
using CleanArchitectureBase.Domain.Common;

namespace CleanArchitectureBase.Application.Contracts.Data
{
    public interface IUnitOfWork
    {
        IUnitOfWorkScope Begin();
        IUnitOfWorkWriteScope BeginWrite();
    }

    public interface IUnitOfWorkScope : IDisposable
    {
        IRepository<TEntity> EntitiesOf<TEntity>()
            where TEntity : EntityBase;
    }

    public interface IUnitOfWorkWriteScope : IUnitOfWorkScope
    {
        Task<int> CompleteAsync();
    }
}
