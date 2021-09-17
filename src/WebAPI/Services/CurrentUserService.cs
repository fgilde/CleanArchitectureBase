using System.Security.Claims;
using CleanArchitectureBase.Application.Contracts;
using Microsoft.AspNetCore.Http;

namespace CleanArchitectureBase.WebAPI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId => User?.FindFirstValue(ClaimTypes.NameIdentifier);
        public ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User;
    }
}
