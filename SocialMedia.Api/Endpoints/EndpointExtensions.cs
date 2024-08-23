using SocialMedia.Api.Endpoints.Account;
using SocialMedia.Api.Endpoints.Auth;

namespace SocialMedia.Api.Endpoints;

public static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder app)
    {
        //call methods for endpoint
        app.MapAuth();
        app.MapAccountEndpoints();
        return app;
    }
}