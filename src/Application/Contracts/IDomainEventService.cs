using System.Threading.Tasks;
using CleanArchitectureBase.Domain.Common;

namespace CleanArchitectureBase.Application.Contracts
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
