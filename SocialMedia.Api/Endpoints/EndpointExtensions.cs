using SocialMedia.Api.Endpoints.Account;
using SocialMedia.Api.Endpoints.Auth;
using SocialMedia.Api.Endpoints.ChatHub;
using SocialMedia.Api.Endpoints.Posts;

namespace SocialMedia.Api.Endpoints;

public static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder app)
    {
        //call methods for endpoint
        app.MapAuth();
        app.MapAccountEndpoints();
        app.MapPostsEndpoints();
        app.MapChatHubEndpoints();
        return app;
    }
}