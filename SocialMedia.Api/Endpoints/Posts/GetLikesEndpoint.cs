using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Posts;

namespace SocialMedia.Api.Endpoints.Posts;

public static class GetLikesEndpoint
{
    public static IEndpointRouteBuilder MapGetLikes(this IEndpointRouteBuilder app)
    {
        app.MapGet("likes", async ([AsParameters]GetLikesQuery request, IMediator mediator, CancellationToken cancellationToken)
            => await mediator.Send(request, cancellationToken));
        return app;
    }
}