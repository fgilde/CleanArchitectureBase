using FluentValidation;
using CleanArchitectureBase.Application.Contracts;
using CleanArchitectureBase.Application.Contracts.Data;
using CleanArchitectureBase.Application.RequestHandling.Base;
using MediatR;

namespace CleanArchitectureBase.Application.RequestHandling.MyEntity
{
    public class CreateMyEntity
    {
        //[Authorize(Policy = Constants.Policies.CanPurge)]
        //[Authorize(Roles = Constants.Roles.Administrator)]
        public class Request : IRequest<Domain.Entities.MyEntity>
        {
            public string Name { get; set; }
        }

        internal class Handler : CreateHandlerBase<Request, Domain.Entities.MyEntity>
        {
            public Handler(IUnitOfWork unitOfWork, IMediator mediator, IApplicationDbContext dbContext) 
                : base(unitOfWork, mediator, dbContext)
            {}

            protected override Domain.Entities.MyEntity CreateEntity(Request request)
            {
                return new Domain.Entities.MyEntity {Name = request.Name};
            }
        }

        internal class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.Name).NotEmpty();
            }
        }

    }
}
