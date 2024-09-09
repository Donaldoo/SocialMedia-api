using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Account;
using SocialMedia.Application.Common;

namespace SocialMedia.Infrastructure.Persistence.User;

internal sealed class GetOnlineUsersHandler : IRequestHandler<GetOnlineUsers, IList<UserDto>>
{
    private readonly AppDbContext _db;
    private readonly ICurrentUser _currentUser;

    public GetOnlineUsersHandler(AppDbContext db, ICurrentUser currentUser)
    {
        _db = db;
        _currentUser = currentUser;
    }

    public async Task<IList<UserDto>> Handle(GetOnlineUsers request, CancellationToken cancellationToken)
    {
        return await _db.Users
            .Where(u => u.IsOnline &&
                        _db.Relationships.Any(r => r.FollowerUserId == _currentUser.UserId && r.FollowedUserId == u.Id) && // Current user follows the other user
                        _db.Relationships.Any(r => r.FollowerUserId == u.Id && r.FollowedUserId == _currentUser.UserId)) // The other user follows the current user back
            .Select(u => new UserDto
            {
                Id = u.Id,
                DisplayName = u.FirstName + " " + u.LastName,
                ProfilePicture = u.ProfilePicture,
            })
            .ToListAsync(cancellationToken);
    }
}