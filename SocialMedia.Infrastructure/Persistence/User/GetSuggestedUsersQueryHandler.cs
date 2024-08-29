using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Account;
using SocialMedia.Application.Common;

namespace SocialMedia.Infrastructure.Persistence.User;

internal sealed class GetSuggestedUsersQueryHandler : IRequestHandler<GetSuggestedUsersQuery, IList<UserDto>>
{
    private readonly AppDbContext _db;
    private readonly ICurrentUser _currentUser;

    public GetSuggestedUsersQueryHandler(AppDbContext db, ICurrentUser currentUser)
    {
        _db = db;
        _currentUser = currentUser;
    }

    public async Task<IList<UserDto>> Handle(GetSuggestedUsersQuery request, CancellationToken cancellationToken)
    {
        return await _db.Users
            .Where(u => u.Id != _currentUser.UserId && !_db.Relationships
                .Any(r => r.FollowerUserId == _currentUser.UserId && r.FollowedUserId == u.Id))

            .Select(u => new UserDto
            {
                Id = u.Id,
                DisplayName = u.FirstName + " " + u.LastName,
                ProfilePicture = u.ProfilePicture
            })
            .ToListAsync(cancellationToken);
    }
}