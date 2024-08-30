using MediatR;
using SocialMedia.Application.Common;
using SocialMedia.Application.Common.Data;
using SocialMedia.Application.Common.Dates;
using SocialMedia.Domain.ChatHub;

namespace SocialMedia.Application.ChatHub.CreateChat;

internal sealed class CreateChatCommandHandler : IRequestHandler<CreateChatCommand, Guid>
{
    private readonly IDataWriter _dataWriter;
    private readonly ICurrentUser _currentUser;
    private readonly IDateTimeFactory _dateTimeFactory;
    private readonly IMediator _mediator;

    public CreateChatCommandHandler(IDataWriter dataWriter, ICurrentUser currentUser, IDateTimeFactory dateTimeFactory, IMediator mediator)
    {
        _dataWriter = dataWriter;
        _currentUser = currentUser;
        _dateTimeFactory = dateTimeFactory;
        _mediator = mediator;
    }

    public async Task<Guid> Handle(CreateChatCommand request, CancellationToken cancellationToken)
    {

        var existingChat = await _mediator.Send(new GetChatByUsersQuery(request.UserId), cancellationToken);
        if (existingChat != Guid.Empty)
        {
            return existingChat;
        }
        
        var chat = new Chat
        {
            CreatedAt = _dateTimeFactory.UtcNowWithOffset(),
        };
        await _dataWriter.InTransactionAsync(async () =>
        {
            await _dataWriter.Add(chat).SaveAsync(cancellationToken);

            await _dataWriter.Add(new ChatUser
            {
                ChatId = chat.Id,
                UserId = _currentUser.UserId,
            }).SaveAsync(cancellationToken);

            await _dataWriter.Add(new ChatUser
            {
                ChatId = chat.Id,
                UserId = request.UserId,
            }).SaveAsync(cancellationToken);
        });
        return chat.Id;
    }
}