using System.Threading;
using System.Threading.Tasks;
using CleanArchitectureBase.Application.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nextended.Core.Extensions;
using NotImplementedException = System.NotImplementedException;

namespace CleanArchitectureBase.Application.RequestHandling.System
{
    public class InitializeApplication
    {
        public class Request : IRequest { }

        internal class Handler : IRequestHandler<Request>
        {
            private readonly IApplicationDbContext _dbContext;
            private readonly IApplicationDbContextSeed _initialSeed;
            private readonly ICurrentUserService _currentUserService;
            private readonly IIdentityService _identityService;

            public Handler(IApplicationDbContext dbContext, IApplicationDbContextSeed initialSeed, ICurrentUserService currentUserService, IIdentityService identityService)
            {
                _dbContext = dbContext;
                _initialSeed = initialSeed;
                _currentUserService = currentUserService;
                _identityService = identityService;
            }
            
            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                if (_dbContext.Database.IsSqlServer())
                {
                    await _dbContext.Database.MigrateAsync(cancellationToken);
                }
                await _initialSeed.SeedDefaultUserAsync();
                await _initialSeed.SeedSampleDataAsync();
                return Unit.Value;
            }
        }
    }
}
