using MediatR;

namespace SocialMedia.Application.ChatHub;

public record GetAllUnreadMessagesByUserIdQuery(Guid UserId) : IRequest<IList<MessageDto>>;