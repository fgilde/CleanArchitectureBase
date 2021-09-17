using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitectureBase.Application.Contracts.Data;
using MediatR;

namespace CleanArchitectureBase.Application.RequestHandling.MyEntity
{
    public class GetMyEntity
    {
        public class Request : IRequest<ReadOnlyCollection<Domain.Entities.MyEntity>>
        { }

        internal class Handler : IRequestHandler<Request, ReadOnlyCollection<Domain.Entities.MyEntity>>
        {
            private readonly IUnitOfWork unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                this.unitOfWork = unitOfWork;
            }

            public Task<ReadOnlyCollection<Domain.Entities.MyEntity>> Handle(Request request, CancellationToken cancellationToken)
            {
                using var scope = this.unitOfWork.Begin();
                return Task.FromResult(scope.EntitiesOf<Domain.Entities.MyEntity>().ToList().AsReadOnly());
            }
        }

    }
}
