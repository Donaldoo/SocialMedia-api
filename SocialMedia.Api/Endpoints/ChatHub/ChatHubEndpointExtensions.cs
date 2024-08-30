namespace SocialMedia.Api.Endpoints.ChatHub;

public static class ChatHubEndpointExtensions
{
    public static IEndpointRouteBuilder MapChatHubEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapCreateChat();
        app.MapCreateMessage();
        app.MapGetChatHistory();
        app.MapGetChatParticipants();

        return app;
    }
    
}