using SocialMedia.Domain.Account;

namespace SocialMedia.Domain.ChatHub;

public record Message
{
    public Guid Id { get; init; }
    public Guid ChatId { get; init; }
    public Chat Chat { get; init; }
    public Guid SenderId { get; init; }
    public User Sender { get; init; }
    public string Content { get; init; }
    public DateTimeOffset SentAt { get; init; } = DateTimeOffset.UtcNow;
}