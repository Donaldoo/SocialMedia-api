using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.ChatHub;

namespace SocialMedia.Infrastructure.Persistence.ChatHub;

internal sealed class GetAllUnreadMessagesByUserIdHandler : IRequestHandler<GetAllUnreadMessagesByUserIdQuery, IList<UnreadMessageDto>>
{
    private readonly AppDbContext _db;

    public GetAllUnreadMessagesByUserIdHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IList<UnreadMessageDto>> Handle(GetAllUnreadMessagesByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _db.Messages
            .Where(m => !m.IsRead && m.SenderId != request.UserId && _db.ChatUsers.Any(cu => cu.ChatId == m.ChatId && cu.UserId == request.UserId))
            .GroupBy(m => m.ChatId)
            .Select(g => new UnreadMessageDto
            {
                Id = g.OrderByDescending(m => m.SentAt).First().Id,
                SenderId = g.OrderByDescending(m => m.SentAt).First().SenderId,
                Content = g.OrderByDescending(m => m.SentAt).First().Content,
                SentAt = g.OrderByDescending(m => m.SentAt).First().SentAt,
                SenderDisplayName = g.OrderByDescending(m => m.SentAt).First().Sender.FirstName + " " +
                                    g.OrderByDescending(m => m.SentAt).First().Sender.LastName,
                SenderProfilePicture = g.OrderByDescending(m => m.SentAt).First().Sender.ProfilePicture,
                UnreadMessageCount = g.Count()
            })
            .OrderByDescending(m => m.SentAt)
            .ToListAsync(cancellationToken);
    }
}