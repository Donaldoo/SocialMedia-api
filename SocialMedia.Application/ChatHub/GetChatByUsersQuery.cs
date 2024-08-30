using MediatR;

namespace SocialMedia.Application.ChatHub;

public record GetChatByUsersQuery(Guid UserId) : IRequest<Guid>;