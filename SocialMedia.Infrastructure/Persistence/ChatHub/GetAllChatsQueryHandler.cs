using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.ChatHub;
using SocialMedia.Application.Common;

namespace SocialMedia.Infrastructure.Persistence.ChatHub;

internal sealed class GetAllChatsQueryHandler : IRequestHandler<GetAllChatsQuery, IList<ChatDto>>
{
    private readonly AppDbContext _db;
    private readonly ICurrentUser _currentUser;

    public GetAllChatsQueryHandler(AppDbContext db, ICurrentUser currentUser)
    {
        _db = db;
        _currentUser = currentUser;
    }

    public async Task<IList<ChatDto>> Handle(GetAllChatsQuery request, CancellationToken cancellationToken)
    {
        return await _db.ChatUsers
            .Where(cu => cu.UserId == _currentUser.UserId)
            .Select(cu => new ChatDto
            {
                ChatId = cu.ChatId,
                UserId = _db.ChatUsers
                    .Where(otherCu => otherCu.ChatId == cu.ChatId && otherCu.UserId != _currentUser.UserId)
                    .Select(otherCu => otherCu.UserId)
                    .FirstOrDefault(),
                UserName = _db.ChatUsers
                    .Where(otherCu => otherCu.ChatId == cu.ChatId && otherCu.UserId != _currentUser.UserId)
                    .Select(otherCu => otherCu.User.FirstName + " " + otherCu.User.LastName)
                    .FirstOrDefault(),
                ProfilePicture = _db.ChatUsers
                    .Where(otherCu => otherCu.ChatId == cu.ChatId && otherCu.UserId != _currentUser.UserId)
                    .Select(otherCu => otherCu.User.ProfilePicture)
                    .FirstOrDefault(),
                LastMessage = _db.Messages
                    .Where(m => m.ChatId == cu.ChatId)
                    .OrderByDescending(m => m.SentAt)
                    .Select(m => m.Content)
                    .FirstOrDefault(),
                LastMessageSentAt = _db.Messages
                    .Where(m => m.ChatId == cu.ChatId)
                    .OrderByDescending(m => m.SentAt)
                    .Select(m => m.SentAt)
                    .FirstOrDefault()
            })
            .OrderByDescending(c => c.LastMessageSentAt)
            .ToListAsync(cancellationToken);
    }
}