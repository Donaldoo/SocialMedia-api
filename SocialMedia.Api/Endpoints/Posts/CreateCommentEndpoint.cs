using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Posts.CreateComment;

namespace SocialMedia.Api.Endpoints.Posts;

public static class CreateCommentEndpoint
{
    public static IEndpointRouteBuilder MapCreateComment(this IEndpointRouteBuilder app)
    {
        app.MapPost("posts/comment", async ([FromBody] CreateCommentCommand request, IMediator mediator)
            => await mediator.Send(request));
        return app;
    }
}