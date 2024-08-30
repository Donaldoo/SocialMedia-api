using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.ChatHub;
using SocialMedia.Application.Common;

namespace SocialMedia.Infrastructure.Persistence.ChatHub;

internal sealed class GetChatByUsersQueryHandler : IRequestHandler<GetChatByUsersQuery,Guid>
{
    private readonly ICurrentUser _currentUser;
    private readonly AppDbContext _db;

    public GetChatByUsersQueryHandler(ICurrentUser currentUser, AppDbContext db)
    {
        _currentUser = currentUser;
        _db = db;
    }

    public async Task<Guid> Handle(GetChatByUsersQuery request, CancellationToken cancellationToken)
    {
        return await _db.ChatUsers
            .Where(cu => cu.UserId == _currentUser.UserId || cu.UserId == request.UserId)
            .GroupBy(cu => cu.ChatId)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .FirstOrDefaultAsync(cancellationToken);
    }
}