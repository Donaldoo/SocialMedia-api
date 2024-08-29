using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Account;

namespace SocialMedia.Api.Endpoints.Account;

public static class GetUserFollowingEndpoint
{
    public static IEndpointRouteBuilder MapGetUserFollowing(this IEndpointRouteBuilder app)
    {
        app.MapGet("user/following", async (IMediator mediator)
            => await mediator.Send(new GetUserFollowingQuery()));
        return app;
    }
}