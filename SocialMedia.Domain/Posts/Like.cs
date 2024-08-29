using SocialMedia.Domain.Account;
using SocialMedia.Domain.Common;

namespace SocialMedia.Domain.Posts;

public record Like : EntityWithGuid
{
    public Guid LikedPostId { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public Post Post { get; init; }
    public Guid LikeUserId { get; init; }
    public User User { get; init; }
}