using MediatR;
using SocialMedia.Application.Common.Data;
using SocialMedia.Domain.Posts;

namespace SocialMedia.Application.Posts.DeletePost;

internal sealed class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, bool>
{
    private readonly IDataWriter _dataWriter;
    private readonly IGenericQuery _genericQuery;

    public DeletePostCommandHandler(IDataWriter dataWriter, IGenericQuery genericQuery)
    {
        _dataWriter = dataWriter;
        _genericQuery = genericQuery;
    }

    public async Task<bool> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var comments = await _genericQuery.GetEntitiesByPropertyAsync<Comment>("PostId", request.PostId);
            var likes = await _genericQuery.GetEntitiesByPropertyAsync<Like>("LikedPostId", request.PostId);

            await _dataWriter.InTransactionAsync(async () =>
            {
                _dataWriter.RemoveRange(comments);
                _dataWriter.RemoveRange(likes);
                await _dataWriter.RemoveAsync<Post>(p => p.Id == request.PostId);
                await _dataWriter.SaveAsync(cancellationToken);
            });
            return true;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}