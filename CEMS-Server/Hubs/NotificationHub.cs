// NotificationHub.cs
using Microsoft.AspNetCore.SignalR;
namespace CEMS_Server.Hubs
{
public class NotificationHub : Hub
{
    public async Task SendNotification()
    {
        await Clients.All.SendAsync("ReceiveNotification");
    }
}
}