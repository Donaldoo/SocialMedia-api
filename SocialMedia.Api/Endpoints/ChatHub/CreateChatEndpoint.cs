using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.ChatHub.CreateChat;

namespace SocialMedia.Api.Endpoints.ChatHub;

public static class CreateChatEndpoint
{
    public static IEndpointRouteBuilder MapCreateChat(this IEndpointRouteBuilder app)
    {
        app.MapPost("chat", async ([AsParameters] CreateChatCommand request, IMediator mediator)
            => await mediator.Send(request));
        return app;
    }
}