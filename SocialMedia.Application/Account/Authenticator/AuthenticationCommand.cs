using MediatR;

namespace SocialMedia.Application.Account.Authenticator;

public record AuthenticationCommand : IRequest<AuthenticationCommandResponse>
{
    public string Email { get; init; }
    public string Password { get; init; }
}

public record AuthenticationCommandResponse
{
    public AuthenticationInfoResponse Data { get; init; }

    public AuthenticationStatus Status { get; init; }
    
    public enum AuthenticationStatus
    {
        Ok,
        Incorrect,
        NotFound,
    }
}
