using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Posts.CreatePost;

namespace SocialMedia.Api.Endpoints.Posts;

public static class CreatePostEndpoint
{
    public static IEndpointRouteBuilder MapCreatePost(this IEndpointRouteBuilder app)
    {
        app.MapPost("posts", async ([FromBody] CreatePostCommand request, IMediator mediator)
            => await mediator.Send(request));
        return app;
    }
}