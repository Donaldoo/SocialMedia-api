using MediatR;
using SocialMedia.Application.ChatHub;

namespace SocialMedia.Api.Endpoints.ChatHub;

public static class GetChatHistoryEndpoint
{
    public static IEndpointRouteBuilder MapGetChatHistory(this IEndpointRouteBuilder app)
    {
        app.MapGet("chat/history", async ([AsParameters] GetChatHistoryQuery request, IMediator mediator)
            => await mediator.Send(request));
        return app;
    }
}