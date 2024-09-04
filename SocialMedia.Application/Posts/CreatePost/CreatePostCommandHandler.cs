using MediatR;
using SocialMedia.Application.Common;
using SocialMedia.Application.Common.Data;
using SocialMedia.Application.Common.Dates;
using SocialMedia.Application.NotificationHub;
using SocialMedia.Domain.Posts;

namespace SocialMedia.Application.Posts.CreatePost;

internal sealed class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Guid>
{
    private readonly IDataWriter _dataWriter;
    private readonly IDateTimeFactory _dateTimeFactory;
    private readonly ICurrentUser _currentUser;
    private readonly ISettings _settings;
    private readonly INotificationService _notificationService;
    private readonly IGenericQuery _genericQuery;

    public CreatePostCommandHandler(IDataWriter dataWriter, IDateTimeFactory dateTimeFactory, ICurrentUser currentUser, ISettings settings, INotificationService notificationService, IGenericQuery genericQuery)
    {
        _dataWriter = dataWriter;
        _dateTimeFactory = dateTimeFactory;
        _currentUser = currentUser;
        _settings = settings;
        _notificationService = notificationService;
        _genericQuery = genericQuery;
    }

    public async Task<Guid> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var post = new Post
        {
            UserId = _currentUser.UserId,
            Description = request.Description,
            Image = _settings.WebApiUrl + request.Image,
            Category = request.Category,
            WorkExperience = request.WorkCategory,
            WorkIndustry = request.WorkIndustry,
            CreatedAt = _dateTimeFactory.UtcNowWithOffset()
        };

        await _dataWriter.Add(post).SaveAsync(cancellationToken);
            
        var followers = await _genericQuery.GetEntitiesByPropertyAsync<Relationship>("FollowedUserId" ,_currentUser.UserId);

        // Notify all followers
        foreach (var follower in followers)
        {
            await _notificationService.NotifyUserOfNewPost(follower.FollowerUserId.ToString(), post.UserId.ToString(), post.Id.ToString());
        }
        return post.Id;
    }
}