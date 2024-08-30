using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.ChatHub.SendMessage;

namespace SocialMedia.Api.Endpoints.ChatHub;

public static class CreateMessageEndpoint
{
    public static IEndpointRouteBuilder MapCreateMessage(this IEndpointRouteBuilder app)
    {
        app.MapPost("chat/message", async ([FromBody] CreateMessageCommand request, IMediator mediator)
            => await mediator.Send(request));
        return app;
    }
}