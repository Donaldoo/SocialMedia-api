using MediatR;

namespace SocialMedia.Application.ChatHub;

public record GetAllUnreadMessagesByUserIdQuery(Guid UserId) : IRequest<IList<UnreadMessageDto>>;

public record UnreadMessageDto
{
    public Guid Id { get; init; }
    public Guid SenderId { get; init; }
    public string Content { get; init; }
    public DateTimeOffset SentAt { get; init; }
    public string SenderDisplayName { get; init; }
    public string SenderProfilePicture { get; init; }
    public int UnreadMessageCount { get; init; }
}