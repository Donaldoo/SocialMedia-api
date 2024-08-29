using MediatR;

namespace SocialMedia.Application.Account.Relationships.Unfollow;

public record UnfollowCommand(Guid UserId) : IRequest<bool>;