using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Account;
using SocialMedia.Application.Common;

namespace SocialMedia.Infrastructure.Persistence.User;

internal sealed class GetUserFollowingQueryHandler : IRequestHandler<GetUserFollowingQuery, IList<UserDto>>
{
    private readonly AppDbContext _db;
    private readonly ICurrentUser _currentUser;

    public GetUserFollowingQueryHandler(AppDbContext db, ICurrentUser currentUser)
    {
        _db = db;
        _currentUser = currentUser;
    }

    public async Task<IList<UserDto>> Handle(GetUserFollowingQuery request, CancellationToken cancellationToken)
    {
        return await _db.Relationships
            .Where(r => r.FollowerUserId == _currentUser.UserId).Select(r => new UserDto
            {
                Id = r.FollowedUser.Id,
                ProfilePicture = r.FollowedUser.ProfilePicture,
                DisplayName = r.FollowedUser.FirstName + " " + r.FollowedUser.LastName,
            })
            .ToListAsync(cancellationToken);
    }
}