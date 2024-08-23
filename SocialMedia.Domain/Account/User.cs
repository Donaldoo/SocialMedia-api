using SocialMedia.Domain.Common;

namespace SocialMedia.Domain.Account;

public record User : EntityWithGuid
{
    public string Username { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public string Fullname { get; init; }
    public string ProfilePicture { get; init; }
    public string CoverPicture { get; init; }
    public string Website { get; init; }
    public string City { get; init; }
}