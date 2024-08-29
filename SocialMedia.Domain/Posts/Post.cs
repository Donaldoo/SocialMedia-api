using SocialMedia.Domain.Account;
using SocialMedia.Domain.Common;

namespace SocialMedia.Domain.Posts;

public record Post : EntityWithGuid
{
    public string Description { get; init; }
    public string Image { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public PostCategory Category { get; init; }
    public Guid UserId { get; init; }
    public User User { get; init; }
}

public enum PostCategory
{
    Work = 1,
    Entertainment,
}