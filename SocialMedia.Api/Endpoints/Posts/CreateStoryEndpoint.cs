using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Posts.CreateStory;

namespace SocialMedia.Api.Endpoints.Posts;

public static class CreateStoryEndpoint
{
    public static IEndpointRouteBuilder MapCreateStory(this IEndpointRouteBuilder app)
    {
        app.MapPost("story/create", async ([AsParameters] CreateStoryCommand request, IMediator mediator)
            => await mediator.Send(request));
        return app;
    }
}