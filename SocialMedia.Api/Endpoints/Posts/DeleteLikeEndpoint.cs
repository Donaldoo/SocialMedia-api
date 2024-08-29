using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Posts.DeleteLike;

namespace SocialMedia.Api.Endpoints.Posts;

public static class DeleteLikeEndpoint
{
    public static IEndpointRouteBuilder MapDeleteLike(this IEndpointRouteBuilder app)
    {
        app.MapDelete("delete/like", async ([AsParameters] DeleteLikeCommand request, IMediator mediator)
            => await mediator.Send(request));
        return app;
    }
}