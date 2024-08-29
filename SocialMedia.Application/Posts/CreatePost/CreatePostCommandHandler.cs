using MediatR;
using SocialMedia.Application.Common;
using SocialMedia.Application.Common.Data;
using SocialMedia.Application.Common.Dates;
using SocialMedia.Domain.Posts;

namespace SocialMedia.Application.Posts.CreatePost;

internal sealed class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Guid>
{
    private readonly IDataWriter _dataWriter;
    private readonly IDateTimeFactory _dateTimeFactory;
    private readonly ICurrentUser _currentUser;
    private readonly ISettings _settings;

    public CreatePostCommandHandler(IDataWriter dataWriter, IDateTimeFactory dateTimeFactory, ICurrentUser currentUser, ISettings settings)
    {
        _dataWriter = dataWriter;
        _dateTimeFactory = dateTimeFactory;
        _currentUser = currentUser;
        _settings = settings;
    }

    public async Task<Guid> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var post = new Post
        {
            UserId = _currentUser.UserId,
            Description = request.Description,
            Image = _settings.WebApiUrl + request.Image,
            Category = request.Category,
            CreatedAt = _dateTimeFactory.UtcNowWithOffset()
        };

        await _dataWriter.Add(post).SaveAsync(cancellationToken);
        return post.Id;
    }
}