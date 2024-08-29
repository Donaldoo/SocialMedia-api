using MediatR;
using SocialMedia.Application.Posts;

namespace SocialMedia.Api.Endpoints.Posts;

public static class GetStoriesEndpoint
{
    public static IEndpointRouteBuilder MapGetStories(this IEndpointRouteBuilder app)
    {
        app.MapGet("stories", async (IMediator mediator)
            => await mediator.Send(new GetStoriesQuery()));
        return app;
    }
}