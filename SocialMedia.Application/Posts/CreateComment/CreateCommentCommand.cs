using MediatR;
using SocialMedia.Application.Common.Requests;

namespace SocialMedia.Application.Posts.CreateComment;

public record CreateCommentCommand : IRequest<Guid>, IAuthenticatedRequest
{
    public Guid PostId { get; init; }
    public string Description { get; init; }
}