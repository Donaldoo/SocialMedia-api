using SocialMedia.Domain.Account;

namespace SocialMedia.Domain.ChatHub;

public record ChatUser
{
    public Guid ChatId { get; init; }
    public Chat Chat { get; init; }
    
    public Guid UserId { get; init; }
    public User User { get; init; }
}