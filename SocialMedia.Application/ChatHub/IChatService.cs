using Microsoft.AspNetCore.SignalR;

namespace SocialMedia.Application.ChatHub;

public interface IChatService
{
    Task NotifyUserOfNewMessage(string recipientUserId, string message);
}

public class ChatService : IChatService
{
    private readonly IHubContext<ChatHub> _hubContext;

    public ChatService(IHubContext<ChatHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task NotifyUserOfNewMessage(string recipientUserId, string message)
    {
        await _hubContext.Clients.User(recipientUserId).SendAsync("ReceiveNotification", message);
    }
}