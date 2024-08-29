using MediatR;
using SocialMedia.Application.Common;

namespace SocialMedia.Application.Posts.UploadImage;

internal sealed class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, string>
{
    private readonly ISettings _settings;

    public UploadImageCommandHandler(ISettings settings)
    {
        _settings = settings;
    }

    public async Task<string> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        if (request.Image == null || request.Image.Length == 0)
            throw new Exception("No file uploaded.");

        var fileName = Guid.NewGuid() + Path.GetExtension(request.Image.FileName);
        var savePath = Path.Combine("wwwroot", "uploads", fileName);
        
        Directory.CreateDirectory(Path.GetDirectoryName(savePath));

        using (var fileStream = new FileStream(savePath, FileMode.Create))
        {
            await request.Image.CopyToAsync(fileStream, cancellationToken);
        }
        
        var imageUrl = Path.Combine("/uploads", fileName).Replace("\\", "/");
        return imageUrl;
    }
}