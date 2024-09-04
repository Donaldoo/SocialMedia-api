using MediatR;

namespace SocialMedia.Application.ChatHub.ReadMessage;

public record ReadMessagesCommand(Guid ChatId, Guid UserId) : IRequest<Guid>;