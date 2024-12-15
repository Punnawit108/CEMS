// NotificationHub.cs
using Microsoft.AspNetCore.SignalR;

namespace CEMS_Server.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SendNotification(string usrId, string message)
        {
            await Clients.User(usrId).SendAsync(message);
        }
    }
}
