using CleanArchitectureBase.Application.Contracts;

namespace CleanArchitectureBase.Application.Hubs
{
    public class ClientEventHub : HubBase
    {
        public ClientEventHub(ISessionProvider sessionProvider) : base(sessionProvider)
        { }
    }
}
