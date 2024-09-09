using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Account;

namespace SocialMedia.Api.Endpoints.Account;

public static class GetUserFollowersEndpoint
{
    public static IEndpointRouteBuilder MapGetUserFollowers(this IEndpointRouteBuilder app)
    {
        app.MapGet("user/followers", async (IMediator mediator)
            => await mediator.Send(new GetUserFollowers()));
        return app;
    }
}