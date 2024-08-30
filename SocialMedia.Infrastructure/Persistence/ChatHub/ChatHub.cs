using MediatR;
using Microsoft.AspNetCore.SignalR;
using SocialMedia.Application.ChatHub;
using SocialMedia.Application.ChatHub.SendMessage;
using SocialMedia.Application.Common;
using SocialMedia.Domain.ChatHub;

namespace SocialMedia.Infrastructure.Persistence.ChatHub;

public class ChatHub : Hub
{
    private readonly IMediator _mediator;

    public ChatHub(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task SendMessage(Guid chatId, Guid userId, string messageContent)
    {
        var message = new Message
        {
            ChatId = chatId,
            Content = messageContent,
            SenderId = userId
        };

        await _mediator.Send(new CreateMessageCommand
        {
            ChatId = chatId,
            Content = messageContent,
            SenderId = userId
        });

        var messageDto = new
        {
            SenderId = userId,
            Content = messageContent,
            SentAt = message.SentAt.ToString("o")
        };

        await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", messageDto);
    }

    public async Task JoinChat(Guid chatId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());

        await Clients.Caller.SendAsync("JoinedChat", chatId);
    }

    public async Task LeaveChat(Guid chatId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId.ToString());

        await Clients.Caller.SendAsync("LeftChat", chatId);
    }

    public async Task GetChatHistory(Guid chatId)
    {
        var query = new GetChatHistoryQuery(chatId);

        var messages = await _mediator.Send(query);

        await Clients.Caller.SendAsync("ReceiveChatHistory", messages);
    }
}