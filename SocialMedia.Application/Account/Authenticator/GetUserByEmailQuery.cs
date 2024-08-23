using MediatR;

namespace SocialMedia.Application.Account.Authenticator;

public record GetUserByEmailQuery : IRequest<Domain.Account.User>
{
    public required string Email { get; init; }
}