using SocialMedia.Domain.Account;
using SocialMedia.Domain.Common;

namespace SocialMedia.Domain.Posts;

public record Relationship : EntityWithGuid
{
    public DateTimeOffset CreatedAt { get; init; }
    public Guid FollowerUserId { get; init; }
    public User FollowerUser { get; init; }
    public Guid FollowedUserId { get; init; }
    public User FollowedUser { get; init; }
}