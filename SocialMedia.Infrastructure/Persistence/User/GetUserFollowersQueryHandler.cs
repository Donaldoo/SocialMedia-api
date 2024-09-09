using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Account;
using SocialMedia.Application.Common;

namespace SocialMedia.Infrastructure.Persistence.User;

internal sealed class GetUserFollowersQueryHandler : IRequestHandler<GetUserFollowers, IList<UserDto>>
{
    private readonly AppDbContext _db;
    private readonly ICurrentUser _currentUser;

    public GetUserFollowersQueryHandler(AppDbContext db, ICurrentUser currentUser)
    {
        _db = db;
        _currentUser = currentUser;
    }

    public async Task<IList<UserDto>> Handle(GetUserFollowers request, CancellationToken cancellationToken)
    {
        return await _db.Relationships
            .Where(r => r.FollowedUserId == _currentUser.UserId).Select(r => new UserDto
            {
                Id = r.FollowerUser.Id,
                ProfilePicture = r.FollowerUser.ProfilePicture,
                DisplayName = r.FollowerUser.FirstName + " " + r.FollowerUser.LastName,
            })
            .ToListAsync(cancellationToken);
    }
}