using MediatR;
using SocialMedia.Application.Posts.DeletePost;

namespace SocialMedia.Api.Endpoints.Posts;

public static class DeletePostEndpoint
{
    public static IEndpointRouteBuilder MapDeletePost(this IEndpointRouteBuilder app)
    {
        app.MapDelete("delete/post", async ([AsParameters] DeletePostCommand request, IMediator mediator)
            => await mediator.Send(request));
        return app;
    }
}