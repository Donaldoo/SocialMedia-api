using MediatR;
using SocialMedia.Application.Common;
using SocialMedia.Application.Common.Data;
using SocialMedia.Application.Common.Dates;
using SocialMedia.Domain.Posts;

namespace SocialMedia.Application.Posts.CreateStory;

internal sealed class CreateStoryCommandHandler: IRequestHandler<CreateStoryCommand, Guid>
{
    private readonly IDataWriter _dataWriter;
    private readonly ICurrentUser _currentUser;
    private readonly IDateTimeFactory _dateTimeFactory;
    private readonly ISettings _settings;

    public CreateStoryCommandHandler(IDataWriter dataWriter, ICurrentUser currentUser, IDateTimeFactory dateTimeFactory, ISettings settings)
    {
        _dataWriter = dataWriter;
        _currentUser = currentUser;
        _dateTimeFactory = dateTimeFactory;
        _settings = settings;
    }

    public async Task<Guid> Handle(CreateStoryCommand request, CancellationToken cancellationToken)
    {
        var story = new Story
        {
            Image = _settings.WebApiUrl + request.Image,
            StoryUserId = _currentUser.UserId,
            CreatedAt = _dateTimeFactory.UtcNowWithOffset()
        };

        await _dataWriter.Add(story).SaveAsync(cancellationToken);
        return story.Id;
    }
}