using SocialMedia.Application.Account.Authenticator;

namespace SocialMedia.Api.Auth;

public interface ITokenGenerator
{
    Task<TokenDto> GenerateAsync(AuthenticationInfoResponse user);
}

public class TokenDto
{
    public string Token { get; set; }
}