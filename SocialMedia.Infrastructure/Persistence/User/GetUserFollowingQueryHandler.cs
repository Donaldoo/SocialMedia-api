using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Account;
using SocialMedia.Application.Common;

namespace SocialMedia.Infrastructure.Persistence.User;

internal sealed class GetUserFollowingQueryHandler : IRequestHandler<GetUserFollowingQuery, IList<Guid>>
{
    private readonly AppDbContext _db;
    private readonly ICurrentUser _currentUser;

    public GetUserFollowingQueryHandler(AppDbContext db, ICurrentUser currentUser)
    {
        _db = db;
        _currentUser = currentUser;
    }

    public async Task<IList<Guid>> Handle(GetUserFollowingQuery request, CancellationToken cancellationToken)
    {
        return await _db.Relationships
            .Where(r => r.FollowerUserId == _currentUser.UserId).Select(r => r.FollowedUserId)
            .ToListAsync(cancellationToken);
    }
}