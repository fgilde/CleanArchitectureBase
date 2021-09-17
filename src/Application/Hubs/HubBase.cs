using System;
using System.Threading.Tasks;
using CleanArchitectureBase.Application.Contracts;
using Microsoft.AspNetCore.SignalR;

namespace CleanArchitectureBase.Application.Hubs
{
    public abstract class HubBase : Hub
    {
        private readonly ISessionProvider sessionProvider;

        protected HubBase(ISessionProvider sessionProvider)
        {
            this.sessionProvider = sessionProvider;
        }

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, sessionProvider.SessionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, sessionProvider.SessionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
