using Microsoft.AspNetCore.SignalR;

namespace Common.Hubs
{
    public class NotifyHub : Hub
    {
        public async Task SubscribeToNotifications(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Caller.SendAsync("SubscriptionConfirmed");
        }
    }
}
