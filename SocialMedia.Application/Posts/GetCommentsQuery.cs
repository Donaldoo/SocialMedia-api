using MediatR;
using SocialMedia.Application.Common.Requests;

namespace SocialMedia.Application.Posts;

public record GetCommentsQuery(Guid PostId) : IRequest<IList<CommentDto>>, IAuthenticatedRequest;

public record CommentDto()
{
    public Guid CommentId { get; init; }
    public string UserImage { get; init; }
    public string UserName { get; init; }
    public string Description { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public bool CanBeDeleted { get; init; }
}