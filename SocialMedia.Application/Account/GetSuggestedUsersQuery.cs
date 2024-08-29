using MediatR;

namespace SocialMedia.Application.Account;

public record GetSuggestedUsersQuery : IRequest<IList<UserDto>>;

public record UserDto
{
    public Guid Id { get; init; }
    public string DisplayName { get; init; }
    public string ProfilePicture { get; init; }
}