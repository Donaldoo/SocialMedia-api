using MediatR;
using SocialMedia.Api.Common.Models;
using SocialMedia.Application.Posts;

namespace SocialMedia.Api.Endpoints.Posts;

public static class GetAllPostsEndpoint
{
    public static IEndpointRouteBuilder MapGetAllPosts(this IEndpointRouteBuilder app)
    {
        app.MapGet("posts", async ([AsParameters]GetAllPosts request, IMediator mediator, CancellationToken cancellationToken)
            => await mediator.Send(request, cancellationToken));
        return app;
    }
}