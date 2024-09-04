using MediatR;
using SocialMedia.Application.Common;
using SocialMedia.Application.Common.Data;
using SocialMedia.Domain.ChatHub;

namespace SocialMedia.Application.ChatHub.ReadMessage;

internal sealed class ReadMessagesCommandHandler : IRequestHandler<ReadMessagesCommand, Guid>
{
    private readonly IDataWriter _dataWriter;
    private readonly IMediator _mediator;
    private readonly ICurrentUser _currentUser;

    public ReadMessagesCommandHandler(IDataWriter dataWriter, IMediator mediator, ICurrentUser currentUser)
    {
        _dataWriter = dataWriter;
        _mediator = mediator;
        _currentUser = currentUser;
    }

    public async Task<Guid> Handle(ReadMessagesCommand request, CancellationToken cancellationToken)
    {
        var messages = await _mediator.Send(new GetUnreadMessagesByChatIdQuery(request.ChatId, request.UserId),
            cancellationToken);

        foreach (var messageId in messages)
        {
            await _dataWriter.UpdateOneAsync<Message>(m => m with
            {
                IsRead = true
            }, messageId);
        }

        await _dataWriter.SaveAsync(cancellationToken);
        return request.ChatId;
    }
}