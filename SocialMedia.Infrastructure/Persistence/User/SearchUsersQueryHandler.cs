using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Account;
using SocialMedia.Application.Common;

namespace SocialMedia.Infrastructure.Persistence.User;

internal sealed class SearchUsersQueryHandler : IRequestHandler<SearchUsersQuery, IList<UserDto>>
{
    private readonly AppDbContext _db;
    private readonly ICurrentUser _currentUser;

    public SearchUsersQueryHandler(AppDbContext db, ICurrentUser currentUser)
    {
        _db = db;
        _currentUser = currentUser;
    }

    public async Task<IList<UserDto>> Handle(SearchUsersQuery request, CancellationToken cancellationToken)
    {
        return await _db.Users
            .Where(u => (u.FirstName.ToLower().Contains(request.Search.ToLower()) ||
                         u.LastName.ToLower().Contains(request.Search.ToLower())) && u.Id != _currentUser.UserId)
            .Select(u => new UserDto
            {
                Id = u.Id,
                DisplayName = u.FirstName + " " + u.LastName,
                ProfilePicture = u.ProfilePicture
            })
            .ToListAsync(cancellationToken);
    }
}