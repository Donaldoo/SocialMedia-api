using MediatR;
using SocialMedia.Application.Common.Requests;

namespace SocialMedia.Application.ChatHub.SendMessage;

public record CreateMessageCommand : IRequest<MessageResponse>
{
    public Guid ChatId { get; init; }
    public string Content { get; init; }
    public Guid SenderId { get; init; }
}

public record MessageResponse
{
    public Guid Id { get; init; }
    public string Content { get; init; }
    public DateTimeOffset SentAt { get; init; }
    public Guid SenderId { get; init; }
}