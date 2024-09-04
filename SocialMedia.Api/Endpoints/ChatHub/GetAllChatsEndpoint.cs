using MediatR;
using SocialMedia.Application.ChatHub;

namespace SocialMedia.Api.Endpoints.ChatHub;

public static class GetAllChatsEndpoint
{
    public static IEndpointRouteBuilder MapGetAllChats(this IEndpointRouteBuilder app)
    {
        app.MapGet("chats", async ([AsParameters] GetAllChatsQuery request, IMediator mediator)
            => await mediator.Send(request));
        return app;
    }
}