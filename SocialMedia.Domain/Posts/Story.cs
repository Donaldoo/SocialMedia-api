using SocialMedia.Domain.Account;
using SocialMedia.Domain.Common;

namespace SocialMedia.Domain.Posts;

public record Story: EntityWithGuid
{
    public string Image { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public Guid StoryUserId { get; init; }
    public User User { get; init; }
}