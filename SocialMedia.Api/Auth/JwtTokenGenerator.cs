using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SocialMedia.Application.Account.Authenticator;

namespace SocialMedia.Api.Auth;

public class JwtTokenGenerator : ITokenGenerator
{
    private readonly IConfiguration _configuration;

    public JwtTokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<TokenDto> GenerateAsync(AuthenticationInfoResponse user)
    {
        return await Task.Run(() =>
        {
            var token = GenerateJwtSecurityToken(user);

            return new TokenDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        });
    }

    private JwtSecurityToken GenerateJwtSecurityToken(AuthenticationInfoResponse user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(user)),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtToken:Key"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        return new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );
    }
}