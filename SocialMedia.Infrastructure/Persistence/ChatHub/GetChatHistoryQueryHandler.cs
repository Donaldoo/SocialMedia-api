using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.ChatHub;

namespace SocialMedia.Infrastructure.Persistence.ChatHub;

public class GetChatHistoryQueryHandler : IRequestHandler<GetChatHistoryQuery, IList<MessageDto>>
{
    private readonly AppDbContext _db;

    public GetChatHistoryQueryHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IList<MessageDto>> Handle(GetChatHistoryQuery request, CancellationToken cancellationToken)
    {
        return await _db.Messages.Where(c => c.ChatId == request.ChatId)
            .Select(c => new MessageDto
            {
                Id = c.Id,
                Content = c.Content,
                SentAt = c.SentAt,
                SenderId = c.SenderId,
                IsRead = c.IsRead
            })
            .OrderBy(c => c.SentAt)
            .ToListAsync(cancellationToken);
    }
}