using MediatR;
using Microsoft.AspNetCore.Http;

namespace SocialMedia.Application.Posts.UploadImage;

public record UploadImageCommand : IRequest<string>
{
    public IFormFile Image { get; init; }
}