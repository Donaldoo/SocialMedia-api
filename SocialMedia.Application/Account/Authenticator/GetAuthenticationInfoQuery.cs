using MediatR;

namespace SocialMedia.Application.Account.Authenticator;

public record GetAuthenticationInfoQuery(Guid UserId) : IRequest<AuthenticationInfoResponse>;

public record AuthenticationInfoResponse
{
    public Guid UserId { get; init; }
    public string Name { get; init; }
}