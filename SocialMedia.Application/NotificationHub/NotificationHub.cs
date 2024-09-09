using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SocialMedia.Application.Common.Data;
using SocialMedia.Domain.Account;

namespace SocialMedia.Application.NotificationHub;

[Authorize]
public class NotificationHub : Hub
{
    private readonly IDataWriter _dataWriter;

    public NotificationHub(IDataWriter dataWriter)
    {
        _dataWriter = dataWriter;
    }

    public async Task SendNotification(string userId, string message)
    {
        Console.WriteLine($"Sent notification to {userId}: {message}");
        await Clients.User(userId).SendAsync("ReceiveNotification", message);
    }
    
    public override async Task OnConnectedAsync()
    {
        var userId = Context.UserIdentifier;
        if (userId != null)
        {
            await _dataWriter.UpdateOneAsync<User>(u => u with
            {
                IsOnline = true
            }, new Guid(userId));
            await _dataWriter.SaveAsync();
            await Clients.All.SendAsync("UserStatusChanged", userId, true);
        }
        await base.OnConnectedAsync();
    }
    
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.UserIdentifier;
        if (userId != null)
        {
            await _dataWriter.UpdateOneAsync<User>(u => u with
            {
                IsOnline = false
            }, new Guid(userId));
            await _dataWriter.SaveAsync();
            await Clients.All.SendAsync("UserStatusChanged", userId, false);
        }

        await base.OnConnectedAsync();
    }
}