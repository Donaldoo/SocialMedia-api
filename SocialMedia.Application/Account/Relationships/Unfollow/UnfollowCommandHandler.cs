using MediatR;
using SocialMedia.Application.Common.Data;
using SocialMedia.Domain.Posts;

namespace SocialMedia.Application.Account.Relationships.Unfollow;

internal sealed class UnfollowCommandHandler: IRequestHandler<UnfollowCommand, bool>
{
    private readonly IDataWriter _dataWriter;
    private readonly IMediator _mediator;

    public UnfollowCommandHandler(IDataWriter dataWriter, IMediator mediator)
    {
        _dataWriter = dataWriter;
        _mediator = mediator;
    }

    public async Task<bool> Handle(UnfollowCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var relationship = await _mediator.Send(new GetRelationshipByUserId(request.UserId), cancellationToken);
            await _dataWriter.RemoveAsync<Relationship>(r => r.Id == relationship);
            await _dataWriter.SaveAsync(cancellationToken);
            return true;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}