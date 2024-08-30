using MediatR;
using SocialMedia.Application.ChatHub;

namespace SocialMedia.Api.Endpoints.ChatHub;

public static class GetChatParticipantsEndpoint
{
    public static IEndpointRouteBuilder MapGetChatParticipants(this IEndpointRouteBuilder app)
    {
        app.MapGet("chat/participants", async ([AsParameters] GetChatParticipantsQuery request, IMediator mediator)
            => await mediator.Send(request));
        return app;
    }
}