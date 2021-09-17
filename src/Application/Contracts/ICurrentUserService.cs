using System.Security.Claims;

namespace CleanArchitectureBase.Application.Contracts
{
    public interface ICurrentUserService
    {
        string UserId { get; }

        ClaimsPrincipal User { get; }
    }
}
