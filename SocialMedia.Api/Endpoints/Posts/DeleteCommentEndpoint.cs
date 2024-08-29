using MediatR;
using SocialMedia.Application.Posts.DeleteComment;

namespace SocialMedia.Api.Endpoints.Posts;

public static class DeleteCommentEndpoint
{
    public static IEndpointRouteBuilder MapDeleteComment(this IEndpointRouteBuilder app)
    {
        app.MapDelete("delete/comment", async ([AsParameters] DeleteCommentCommand request, IMediator mediator)
            => await mediator.Send(request));
        return app;
    }
}