using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.ChatHub;

namespace SocialMedia.Infrastructure.Persistence.ChatHub;

internal sealed class GetUnreadMessagesByChatIdQueryHandler : IRequestHandler<GetUnreadMessagesByChatIdQuery, IList<Guid>>
{
    private readonly AppDbContext _db;

    public GetUnreadMessagesByChatIdQueryHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IList<Guid>> Handle(GetUnreadMessagesByChatIdQuery request, CancellationToken cancellationToken)
    {
       return await _db.Messages
            .Where(m => m.ChatId == request.ChatId && m.SenderId != request.UserId && !m.IsRead)
            .Select(m => m.Id)
            .ToListAsync(cancellationToken);
    }
}