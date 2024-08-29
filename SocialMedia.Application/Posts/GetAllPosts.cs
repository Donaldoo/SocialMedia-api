using MediatR;
using SocialMedia.Application.Common.Requests;
using SocialMedia.Domain.Posts;

namespace SocialMedia.Application.Posts;

public record GetAllPosts() : IRequest<IList<PostDto>>, IAuthenticatedRequest
{
    public PostCategory? Category { get; init; }
    public Guid? UserId { get; init; }
}

public record PostDto()
{
    public Guid Id { get; init; }
    public string Description { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public string Image { get; init; }
    public Guid UserId { get; init; }
    public string FullName { get; init; }
    public string ProfilePicture { get; init; }
}