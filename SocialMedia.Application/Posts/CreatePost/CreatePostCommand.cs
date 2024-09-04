using MediatR;
using SocialMedia.Application.Common.Requests;
using SocialMedia.Domain.Posts;

namespace SocialMedia.Application.Posts.CreatePost;

public record CreatePostCommand : IRequest<Guid>, IAuthenticatedRequest
{
    public string Description { get; init; }
    public string Image { get; init; }
    public PostCategory Category { get; init; }
    public WorkExperience? WorkCategory { get; init; }
    public WorkIndustry? WorkIndustry { get; init; }
}