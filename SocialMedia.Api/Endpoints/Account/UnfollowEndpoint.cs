using MediatR;
using SocialMedia.Application.Account.Relationships.Unfollow;

namespace SocialMedia.Api.Endpoints.Account;

public static class UnfollowEndpoint
{
    public static IEndpointRouteBuilder MapUnfollow(this IEndpointRouteBuilder app)
    {
        app.MapPost("unfollow", async ([AsParameters] UnfollowCommand request, IMediator mediator)
            => await mediator.Send(request));
        return app;
    }
}