namespace SocialMedia.Api.Endpoints.Account;

public static class AccountEndpointExtensions 
{
    public static IEndpointRouteBuilder MapAccountEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapCreateAccount();
        app.MapCreateFollow();
        app.MapUnfollow();
        app.MapGetUserById();
        app.MapEditAccount();
        app.MapGetSuggestedUsers();
        app.MapGetUserFollowing();
        return app;
    }
}