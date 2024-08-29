using MediatR;

namespace SocialMedia.Application.Account.Relationships.Follow;

public record CreateFollowCommand(Guid UserId) : IRequest<Guid>;