using MediatR;

namespace SocialMedia.Application.ChatHub.CreateChat;

public record CreateChatCommand : IRequest<Guid>
{
    public Guid UserId { get; init; }
}