using MediatR;
using SocialMedia.Application.Account;

namespace SocialMedia.Api.Endpoints.Account;

public static class GetOnlineUsersEndpoint
{
    public static IEndpointRouteBuilder MapGetOnlineUsers(this IEndpointRouteBuilder app)
    {
        app.MapGet("users/online", async (IMediator mediator)
            => await mediator.Send(new GetOnlineUsers()));
        return app;
    }
}