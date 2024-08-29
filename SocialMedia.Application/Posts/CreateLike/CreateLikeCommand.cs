using MediatR;
using SocialMedia.Application.Common.Requests;

namespace SocialMedia.Application.Posts.CreateLike;

public record CreateLikeCommand : IRequest<Guid>, IAuthenticatedRequest
{
    public Guid PostId { get; init; }
};