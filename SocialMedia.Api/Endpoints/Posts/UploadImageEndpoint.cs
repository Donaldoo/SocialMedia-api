using MediatR;
using SocialMedia.Application.Posts.UploadImage;

namespace SocialMedia.Api.Endpoints.Posts;

public static class UploadImageEndpoint
{
    public static IEndpointRouteBuilder MapUploadImage(this IEndpointRouteBuilder app)
    {
        app.MapPost("upload", async (HttpRequest httpRequest, IMediator mediator) =>
            {
                var formFile = httpRequest.Form.Files.GetFile("Image");
                if (formFile == null)
                {
                    return Results.BadRequest("Image is required.");
                }

                var command = new UploadImageCommand
                {
                    Image = formFile
                };

                var result = await mediator.Send(command);
                return Results.Ok(result);
            })
            .Accepts<UploadImageCommand>("multipart/form-data");

        return app;
    }
}