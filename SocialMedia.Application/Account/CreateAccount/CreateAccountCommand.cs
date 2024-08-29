using MediatR;
using SocialMedia.Application.Account.Authenticator;

namespace SocialMedia.Application.Account.CreateAccount;

public record CreateAccountCommand : IRequest<AuthenticationInfoResponse>
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public string City { get; init; }
    public string Website { get; init; }
}