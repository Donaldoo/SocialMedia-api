using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Account;

namespace SocialMedia.Api.Endpoints.Account;

public static class GetSuggestedUsersEndpoint
{
    public static IEndpointRouteBuilder MapGetSuggestedUsers(this IEndpointRouteBuilder app)
    {
        app.MapGet("user/suggested", async (IMediator mediator)
            => await mediator.Send(new GetSuggestedUsersQuery()));
        return app;
    }
}