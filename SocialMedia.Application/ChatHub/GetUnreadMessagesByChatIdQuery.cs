using MediatR;
using SocialMedia.Application.Common.Requests;

namespace SocialMedia.Application.ChatHub;

public record GetUnreadMessagesByChatIdQuery(Guid ChatId, Guid UserId) : IRequest<IList<Guid>>;