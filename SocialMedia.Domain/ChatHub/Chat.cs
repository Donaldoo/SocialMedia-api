namespace SocialMedia.Domain.ChatHub;

public record Chat
{
    public Guid Id { get; init; }
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
}