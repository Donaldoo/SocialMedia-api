using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Account;

namespace SocialMedia.Infrastructure.Persistence.User;

internal sealed class GetUserByIdEfHandler : IRequestHandler<GetUserById, UserDetails>
{
    private readonly AppDbContext _db;

    public GetUserByIdEfHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<UserDetails> Handle(GetUserById request, CancellationToken cancellationToken)
    {
        return await _db.Users.Where(u => u.Id == request.UserId).Select(u => new UserDetails
        {
            Id = u.Id,
            FirstName = u.FirstName,
            LastName = u.LastName,
            Email = u.Email,
            ProfilePicture = u.ProfilePicture,
            CoverPicture = u.CoverPicture,
            Bio = u.Bio,
            City = u.City,
            IsOnline = u.IsOnline,
            FollowingCount = _db.Relationships.Count(r => r.FollowerUserId == u.Id),
            FollowersCount = _db.Relationships.Count(r => r.FollowedUserId == u.Id)
        }).SingleOrDefaultAsync(cancellationToken);
    }
}