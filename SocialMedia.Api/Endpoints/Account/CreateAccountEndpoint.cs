using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Auth;
using SocialMedia.Application.Account.CreateAccount;

namespace SocialMedia.Api.Endpoints.Account;

public static class CreateAccountEndpoint
{

    public static IEndpointRouteBuilder MapCreateAccount(this IEndpointRouteBuilder app)
    {
        app.MapPost("account/create", async ([FromBody] CreateAccountCommand request, IMediator mediator,  ITokenGenerator tokenGenerator) =>
        {
           var account = await mediator.Send(request);
           return await tokenGenerator.GenerateAsync(account);
        });
        
        return app;
    }
}