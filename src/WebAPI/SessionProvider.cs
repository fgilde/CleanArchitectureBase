using System;
using CleanArchitectureBase.Application;
using CleanArchitectureBase.Application.Contracts;
using Microsoft.AspNetCore.Http;

namespace CleanArchitectureBase.WebAPI
{
    public class SessionProvider : ISessionProvider
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public SessionProvider(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
            SessionId = httpContextAccessor?.HttpContext?.Session.GetString(Constants.SessionIdKey) ?? Guid.NewGuid().ToString();
        }

        public string SessionId { get; set; }
    }
}
