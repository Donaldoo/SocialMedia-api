using MediatR;
using SocialMedia.Application.Common.Data;
using SocialMedia.Application.Common.Dates;
using SocialMedia.Domain.ChatHub;

namespace SocialMedia.Application.ChatHub.SendMessage;

internal sealed class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, MessageResponse>
{
    private readonly IDataWriter _dataWriter;
    private readonly IDateTimeFactory _dateTimeFactory;

    public CreateMessageCommandHandler(IDataWriter dataWriter, IDateTimeFactory dateTimeFactory)
    {
        _dataWriter = dataWriter;
        _dateTimeFactory = dateTimeFactory;
    }

    public async Task<MessageResponse> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        var message = new Message
        {
            ChatId = request.ChatId,
            Content = request.Content,
            SentAt = _dateTimeFactory.UtcNowWithOffset(),
            SenderId = request.SenderId,
        };

        await _dataWriter.Add(message).SaveAsync(cancellationToken);
        return new MessageResponse
        {
            Id = message.Id,
            Content = message.Content,
            SentAt = message.SentAt,
            SenderId = message.SenderId,
        };
    }
}