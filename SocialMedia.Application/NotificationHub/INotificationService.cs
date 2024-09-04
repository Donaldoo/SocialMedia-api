using Microsoft.AspNetCore.SignalR;
using SocialMedia.Application.Common.Data;
using SocialMedia.Domain.Account;

namespace SocialMedia.Application.NotificationHub;

public interface INotificationService
{
    Task NotifyUserOfNewFollower(string followerId, string followedUserId);
    Task NotifyUserOfNewPost(string userId, string userPostedId, string postId);
}

public class NotificationService : INotificationService
{
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly IGenericQuery _genericQuery;

    public NotificationService(IHubContext<NotificationHub> hubContext, IGenericQuery genericQuery)
    {
        _hubContext = hubContext;
        _genericQuery = genericQuery;
    }

    public async Task NotifyUserOfNewFollower(string followerId, string followedUserId)
    {
        var user = await _genericQuery.GetByIdAsync<User>(new Guid(followerId));
        var message = $"{user.FirstName} {user.LastName} started following you.";
        await _hubContext.Clients.User(followedUserId).SendAsync("ReceiveNotification", new { type = "follow", message});
    }

    public async Task NotifyUserOfNewPost(string userId, string userPostedId, string postId)
    {
        var user = await _genericQuery.GetByIdAsync<User>(new Guid(userPostedId));
        var message = $"{user.FirstName} {user.LastName} posted something new!";
        await _hubContext.Clients.User(userId).SendAsync("ReceiveNotification", new {type = "post", message, postId });
    }
}