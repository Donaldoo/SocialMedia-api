using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Auth;
using SocialMedia.Application.Account.Authenticator;

namespace SocialMedia.Api.Endpoints.Auth;

public static class AuthEndpointExtension
{
    public static IEndpointRouteBuilder MapAuth(this IEndpointRouteBuilder app)
    {
        app.MapPost("/auth", async ([FromBody] AuthenticationCommand request, IMediator mediator,  ITokenGenerator tokenGenerator) =>
        {
            var result = await mediator.Send(request);
            var token = result.Data != null ? (await tokenGenerator.GenerateAsync(result.Data)).Token : string.Empty;
            return new AuthResponse
            {
                Token = token,
                User = result.Data,
                Status = result.Status
            };

        });
        return app;
    }
    
    public record AuthResponse
    {
        public string Token { get; init; }
        public AuthenticationInfoResponse User { get; init; }
        public AuthenticationCommandResponse.AuthenticationStatus Status { get; init; }
    }
}