using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Auth;
using SocialMedia.Application.Account.EditAccount;

namespace SocialMedia.Api.Endpoints.Account;

public static class EditAccountEndpoint
{
    public static IEndpointRouteBuilder MapEditAccount(this IEndpointRouteBuilder app)
    {
        app.MapPost("account/edit", async ([FromBody] EditAccountCommand request, IMediator mediator)
            => await mediator.Send(request));
        return app;
    }
}