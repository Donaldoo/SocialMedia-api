using MediatR;
using SocialMedia.Application.Common.Data;
using SocialMedia.Domain.Posts;

namespace SocialMedia.Application.Posts.DeleteLike;

internal sealed class DeleteLikeCommandHandler : IRequestHandler<DeleteLikeCommand, bool>
{
    private readonly IDataWriter _dataWriter;
    private readonly IMediator _mediator;

    public DeleteLikeCommandHandler(IDataWriter dataWriter, IMediator mediator)
    {
        _dataWriter = dataWriter;
        _mediator = mediator;
    }

    public async Task<bool> Handle(DeleteLikeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var like = await _mediator.Send(new GetLikeByPostAndUserIdQuery(request.PostId), cancellationToken);
            await _dataWriter.RemoveAsync<Like>(l => l.Id == like);
            await _dataWriter.SaveAsync(cancellationToken);
            return true;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}