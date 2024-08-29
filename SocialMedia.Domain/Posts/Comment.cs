using SocialMedia.Domain.Account;
using SocialMedia.Domain.Common;

namespace SocialMedia.Domain.Posts;

public record Comment : EntityWithGuid
{
    public string Description { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public Guid CommentUserId { get; init; }
    public User User { get; init; }
    public Guid PostId { get; init; }
    public Post Post { get; init; }
}