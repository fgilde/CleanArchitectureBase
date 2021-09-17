using System.Collections.Generic;
using CleanArchitectureBase.Domain.Common;

namespace CleanArchitectureBase.Domain.Entities
{
    public class MyEntity : AuditableEntity, IHasDomainEvent
    {
        public string Name { get; set; }

        public List<DomainEvent> DomainEvents { get; set; } = new();
    }
}
