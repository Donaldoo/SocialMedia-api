using MediatR;
using SocialMedia.Application.ChatHub;

namespace SocialMedia.Api.Endpoints.ChatHub;

public static class GetAllUnreadMessagesEndpoint
{
    public static IEndpointRouteBuilder MapGetAllUnreadMessages(this IEndpointRouteBuilder app)
    {
        app.MapGet("chats/unread-messages", async ([AsParameters] GetAllUnreadMessagesByUserIdQuery request, IMediator mediator)
            => await mediator.Send(request));
        return app;
    }
}