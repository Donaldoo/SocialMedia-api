using MediatR;
using SocialMedia.Application.Common;
using SocialMedia.Application.Common.Data;
using SocialMedia.Application.Common.Dates;
using SocialMedia.Domain.ChatHub;

namespace SocialMedia.Application.ChatHub.SendMessage;

internal sealed class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, MessageResponse>
{
    private readonly IDataWriter _dataWriter;
    private readonly IDateTimeFactory _dateTimeFactory;
    private readonly IChatService _chatService;
    private readonly ICurrentUser _currentUser;
    private readonly IMediator _mediator;

    public CreateMessageCommandHandler(IDataWriter dataWriter, IDateTimeFactory dateTimeFactory,
        IChatService chatService, ICurrentUser currentUser, IMediator mediator)
    {
        _dataWriter = dataWriter;
        _dateTimeFactory = dateTimeFactory;
        _chatService = chatService;
        _currentUser = currentUser;
        _mediator = mediator;
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

        var users = await _mediator.Send(new GetChatParticipantsQuery(request.ChatId), cancellationToken);

        foreach (var user in users)
        {
            await _chatService.NotifyUserOfNewMessage(user.Id.ToString(), message.Content);
        }

        return new MessageResponse
        {
            Id = message.Id,
            Content = message.Content,
            SentAt = message.SentAt,
            SenderId = message.SenderId,
        };
    }
}