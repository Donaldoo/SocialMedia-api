using MediatR;

namespace SocialMedia.Application.ChatHub;

public record GetAllChatsQuery : IRequest<IList<ChatDto>>
{
    public string Search { get; init; }
}

public record ChatDto
{
    public Guid? ChatId { get; init; }
    public Guid UserId { get; init; }
    public string UserName { get; init; }
    public string ProfilePicture { get; init; }
    public string LastMessage { get; init; }
    public DateTimeOffset LastMessageSentAt { get; init; }
    public bool IsOnline { get; init; }
}