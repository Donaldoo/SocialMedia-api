using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SocialMedia.Application.ChatHub.ReadMessage;
using SocialMedia.Application.ChatHub.SendMessage;

namespace SocialMedia.Application.ChatHub;

[Authorize]
public class ChatHub : Hub
{
    private readonly IMediator _mediator;

    public ChatHub(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task JoinChat(string chatId, string userId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
        await _mediator.Send(new ReadMessagesCommand(new Guid(chatId), new Guid(userId)));
    }

    public async Task LeaveChat(string chatId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId);
    }

    public async Task SendMessage(string chatId, string userId, string message)
    {
        var newMessage = await _mediator.Send(new CreateMessageCommand{ChatId = new Guid(chatId), SenderId = new Guid(userId), Content = message});
        
        // Broadcast the message to all users in the chat group
        await Clients.Group(chatId).SendAsync("ReceiveMessage", new { chatId, newMessage });
    }

    public async Task SendMessageNotification(string recipientUserId, string message)
    {
        await Clients.User(recipientUserId).SendAsync("ReceiveNotification", message);
    }
    
    public async Task GetChatHistory(string chatId)
    {
        var messages = await _mediator.Send(new GetChatHistoryQuery(new Guid(chatId)));

        await Clients.Caller.SendAsync("ReceiveChatHistory", messages);
    }
}