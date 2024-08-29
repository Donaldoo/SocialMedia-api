using MediatR;
using SocialMedia.Application.Common;
using SocialMedia.Application.Common.Data;
using SocialMedia.Application.Common.Dates;
using SocialMedia.Domain.Posts;

namespace SocialMedia.Application.Posts.CreateComment;

internal sealed class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Guid>
{
    private readonly ICurrentUser _currentUser;
    private readonly IDateTimeFactory _dateTimeFactory;
    private readonly IDataWriter _dataWriter;

    public CreateCommentCommandHandler(ICurrentUser currentUser, IDateTimeFactory dateTimeFactory, IDataWriter dataWriter)
    {
        _currentUser = currentUser;
        _dateTimeFactory = dateTimeFactory;
        _dataWriter = dataWriter;
    }

    public async Task<Guid> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = new Comment
        {
            Id = Guid.NewGuid(),
            PostId = request.PostId,
            CommentUserId  = _currentUser.UserId,
            Description = request.Description,
            CreatedAt = _dateTimeFactory.UtcNowWithOffset()
        };

        await _dataWriter.Add(comment).SaveAsync(cancellationToken);
        return comment.Id;
    }
}