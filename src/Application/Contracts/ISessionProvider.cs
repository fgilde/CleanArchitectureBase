using System;

namespace CleanArchitectureBase.Application.Contracts
{
    public interface ISessionProvider
    {
        string SessionId { get; }
    }

    public class SimpleSessionProvider : ISessionProvider
    {
        public SimpleSessionProvider(ICurrentUserService userService)
        {
            SessionId = userService?.UserId ?? Guid.NewGuid().ToString();
        }

        public string SessionId { get; }
    }
}
