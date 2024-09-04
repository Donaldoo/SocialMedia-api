using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.ChatHub;

namespace SocialMedia.Infrastructure.Persistence.ChatHub;

internal sealed class GetAllUnreadMessagesByUserIdHandler : IRequestHandler<GetAllUnreadMessagesByUserIdQuery, IList<MessageDto>>
{
    private readonly AppDbContext _db;

    public GetAllUnreadMessagesByUserIdHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IList<MessageDto>> Handle(GetAllUnreadMessagesByUserIdQuery request, CancellationToken cancellationToken)
    {
        return await _db.Messages
            .Where(m => !m.IsRead && _db.ChatUsers.Any(cu => cu.ChatId == m.ChatId && cu.UserId == request.UserId))
            .Select(m => new MessageDto
            {
                Id = m.Id,
                SenderId = m.SenderId,
                Content = m.Content,
                SentAt = m.SentAt
            })
            .OrderByDescending(m => m.SentAt)
            .ToListAsync(cancellationToken);
    }
}