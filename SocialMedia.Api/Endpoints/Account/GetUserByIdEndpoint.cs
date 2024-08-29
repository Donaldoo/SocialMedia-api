using MediatR;
using SocialMedia.Application.Account;

namespace SocialMedia.Api.Endpoints.Account;

public static class GetUserByIdEndpoint
{
    public static IEndpointRouteBuilder MapGetUserById(this IEndpointRouteBuilder app)
    {
        app.MapGet("user", async ([AsParameters] GetUserById request, IMediator mediator)
            => await mediator.Send(request));
        return app;
    }
}