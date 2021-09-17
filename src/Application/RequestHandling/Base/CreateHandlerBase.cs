using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitectureBase.Application.Contracts;
using CleanArchitectureBase.Application.Contracts.Data;
using CleanArchitectureBase.Domain.Common;
using CleanArchitectureBase.Domain.Events;
using MediatR;

namespace CleanArchitectureBase.Application.RequestHandling.Base
{

    internal abstract class CreateHandlerBase<TRequest, TEntity> : IRequestHandler<TRequest, TEntity>
        where TRequest : IRequest<TEntity>
        where TEntity : EntityBase
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IApplicationDbContext DbContext;
        protected readonly IMediator Mediator;

        protected CreateHandlerBase(IUnitOfWork unitOfWork, IMediator mediator, IApplicationDbContext dbContext)
        {
            this.UnitOfWork = unitOfWork;
            this.DbContext = dbContext;
            this.Mediator = mediator;
        }

        public virtual async Task<TEntity> Handle(TRequest request, CancellationToken cancellationToken)
        {
            using var scope = UnitOfWork.BeginWrite();
            var r = scope.EntitiesOf<TEntity>().Insert(CreateEntity(request));
            await scope.CompleteAsync();
            var result = scope.EntitiesOf<TEntity>().FirstOrDefault(entity => entity.Id == r.Value);
            var asDomainEvents = result as IHasDomainEvent;
            asDomainEvents?.DomainEvents.Add(new EntityCreatedEvent<TEntity>(result));
            await Mediator.Publish(new ClientEvent(TargetClient.Current, "Entity Created", result), cancellationToken);
            return result;
        }

        protected abstract TEntity CreateEntity(TRequest request);
    }
}
