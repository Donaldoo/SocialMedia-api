using MediatR;
using SocialMedia.Application.Account.Relationships.Follow;

namespace SocialMedia.Api.Endpoints.Account;

public static class CreateFollowEndpoint
{
    public static IEndpointRouteBuilder MapCreateFollow(this IEndpointRouteBuilder app)
    {
        app.MapPost("follow", async ([AsParameters] CreateFollowCommand request, IMediator mediator)
            => await mediator.Send(request));
        return app;
    }
}