namespace SocialMedia.Application.Common;

public interface ICurrentUser
{
    Guid UserId { get; }
}


public record CurrentUser : ICurrentUser
{
    public Guid UserId { get; init; }
}