using MediatR;
using SocialMedia.Application.Common;
using SocialMedia.Application.Common.Data;
using SocialMedia.Application.Common.Dates;
using SocialMedia.Domain.Posts;

namespace SocialMedia.Application.Account.Relationships.Follow;

internal sealed class CreateFollowCommandHandler : IRequestHandler<CreateFollowCommand, Guid>
{
    private readonly ICurrentUser _currentUser;
    private readonly IDateTimeFactory _dateTimeFactory;
    private readonly IDataWriter _dataWriter;

    public CreateFollowCommandHandler(ICurrentUser currentUser, IDateTimeFactory dateTimeFactory, IDataWriter dataWriter)
    {
        _currentUser = currentUser;
        _dateTimeFactory = dateTimeFactory;
        _dataWriter = dataWriter;
    }

    public async Task<Guid> Handle(CreateFollowCommand request, CancellationToken cancellationToken)
    {
        var follow = new Relationship
        {
            FollowerUserId = _currentUser.UserId,
            FollowedUserId = request.UserId,
            CreatedAt = _dateTimeFactory.UtcNowWithOffset()
        };

        await _dataWriter.Add(follow).SaveAsync(cancellationToken);
        return follow.Id;
    }
}