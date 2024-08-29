using MediatR;
using SocialMedia.Application.Posts;

namespace SocialMedia.Api.Endpoints.Posts;

public static class GetCommentsEndpoint
{
    public static IEndpointRouteBuilder MapGetComments(this IEndpointRouteBuilder app)
    {
        app.MapGet("comments", async ([AsParameters]GetCommentsQuery request, IMediator mediator, CancellationToken cancellationToken)
            => await mediator.Send(request, cancellationToken));
        return app;
    }
}