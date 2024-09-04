using MediatR;
using SocialMedia.Application.Account;

namespace SocialMedia.Api.Endpoints.Account;

public static class SearchUsersEndpoint
{
    public static IEndpointRouteBuilder MapSearchUsers(this IEndpointRouteBuilder app)
    {
        app.MapGet("search/user", async ([AsParameters] SearchUsersQuery request, IMediator mediator)
            => await mediator.Send(request));
        return app;
    }
}