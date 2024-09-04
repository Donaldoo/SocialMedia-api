using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace SocialMedia.Application.NotificationHub;

[Authorize]
public class NotificationHub : Hub
{
    public async Task SendNotification(string userId, string message)
    {
        Console.WriteLine($"Sent notification to {userId}: {message}");
        await Clients.User(userId).SendAsync("ReceiveNotification", message);
    }
}