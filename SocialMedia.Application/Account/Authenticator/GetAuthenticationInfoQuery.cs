using MediatR;

namespace SocialMedia.Application.Account.Authenticator;

public record GetAuthenticationInfoQuery(Guid UserId) : IRequest<AuthenticationInfoResponse>;

public record AuthenticationInfoResponse
{
    public Guid UserId { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string ProfilePicture { get; init; }
}