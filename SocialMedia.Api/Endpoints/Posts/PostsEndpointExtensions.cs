namespace SocialMedia.Api.Endpoints.Posts;

public static class PostsEndpointExtensions
{
    public static IEndpointRouteBuilder MapPostsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapCreatePost();
        app.MapCreateLike();
        app.MapDeleteLike();
        app.MapCreateComment();
        app.MapDeleteComment();
        app.MapGetAllPosts();
        app.MapGetLikes();
        app.MapGetComments();
        app.MapDeletePost();
        app.MapUploadImage();
        app.MapCreateStory();
        app.MapGetStories();
        app.MapGetPostById();
        return app;
    }
}