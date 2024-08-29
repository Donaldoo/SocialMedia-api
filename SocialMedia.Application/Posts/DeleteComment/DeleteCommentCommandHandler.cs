using MediatR;
using SocialMedia.Application.Common.Data;
using SocialMedia.Domain.Posts;

namespace SocialMedia.Application.Posts.DeleteComment;

internal sealed class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, bool>
{
    private readonly IDataWriter _dataWriter;
    private readonly IMediator _mediator;

    public DeleteCommentCommandHandler(IDataWriter dataWriter, IMediator mediator)
    {
        _dataWriter = dataWriter;
        _mediator = mediator;
    }

    public async Task<bool> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            //var comment = await _mediator.Send(new GetCommentByPostAndUserIdQuery(request.PostId), cancellationToken);
            await _dataWriter.RemoveAsync<Comment>(l => l.Id == request.CommentId);
            await _dataWriter.SaveAsync(cancellationToken);
            return true;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}