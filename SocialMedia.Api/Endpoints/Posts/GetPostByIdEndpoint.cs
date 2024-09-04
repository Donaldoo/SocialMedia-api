using MediatR;
using SocialMedia.Application.Posts;

namespace SocialMedia.Api.Endpoints.Posts;

public static class GetPostByIdEndpoint
{
    public static IEndpointRouteBuilder MapGetPostById(this IEndpointRouteBuilder app)
    {
        app.MapGet("post", async ([AsParameters] GetPostByIdQuery request, IMediator mediator)
            => await mediator.Send(request));
        return app;
    }
}