using MediatR;
using SocialMedia.Application.Common.Requests;

namespace SocialMedia.Application.Posts;

public record GetLikesQuery(Guid PostId) : IRequest<IList<LikeDto>>, IAuthenticatedRequest;

public record LikeDto()
{
    public Guid LikeId { get; init; }
    public Guid UserId { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
}