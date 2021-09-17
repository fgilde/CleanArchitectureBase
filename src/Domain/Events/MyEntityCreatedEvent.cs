using CleanArchitectureBase.Domain.Common;

namespace CleanArchitectureBase.Domain.Events
{
    public class EntityCreatedEvent<TEntity> : DomainEvent
        where TEntity: EntityBase
    {
        public EntityCreatedEvent(TEntity item)
        {
            Item = item;
        }

        public TEntity Item { get; }
    }
}
