namespace SocialMedia.Domain.Common;

public record EntityWithGuid : Entity<Guid>
{
    protected EntityWithGuid()
    {
        Id = Guid.NewGuid();
    }
}