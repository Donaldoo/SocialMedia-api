using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Posts.CreateLike;

namespace SocialMedia.Api.Endpoints.Posts;

public static class CreateLikeEndpoint
{
    public static IEndpointRouteBuilder MapCreateLike(this IEndpointRouteBuilder app)
    {
        app.MapPost("posts/like", async ([AsParameters] CreateLikeCommand request, IMediator mediator)
            => await mediator.Send(request));
        return app;
    }
}