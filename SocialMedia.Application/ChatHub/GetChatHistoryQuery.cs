using MediatR;

namespace SocialMedia.Application.ChatHub;

public record GetChatHistoryQuery(Guid ChatId) : IRequest<IList<MessageDto>>;

public record MessageDto
{
    public Guid Id { get; init; }
    public string Content { get; init; }
    public DateTimeOffset SentAt { get; init; }
    public Guid SenderId { get; init; }
}