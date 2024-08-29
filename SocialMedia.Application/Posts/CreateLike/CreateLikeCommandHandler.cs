using MediatR;
using SocialMedia.Application.Common;
using SocialMedia.Application.Common.Data;
using SocialMedia.Application.Common.Dates;
using SocialMedia.Domain.Posts;

namespace SocialMedia.Application.Posts.CreateLike;

internal sealed class CreateLikeCommandHandler : IRequestHandler<CreateLikeCommand, Guid>
{
    private readonly IDataWriter _dataWriter;
    private readonly ICurrentUser _currentUser;
    private readonly IDateTimeFactory _dateTimeFactory;

    public CreateLikeCommandHandler(IDataWriter dataWriter, ICurrentUser currentUser, IDateTimeFactory dateTimeFactory)
    {
        _dataWriter = dataWriter;
        _currentUser = currentUser;
        _dateTimeFactory = dateTimeFactory;
    }

    public async Task<Guid> Handle(CreateLikeCommand request, CancellationToken cancellationToken)
    {
        var like = new Like
        {
            CreatedAt = _dateTimeFactory.UtcNowWithOffset(),
            LikeUserId = _currentUser.UserId,
            LikedPostId = request.PostId,
        };

        await _dataWriter.Add(like).SaveAsync(cancellationToken);
        return like.Id;
    }
}