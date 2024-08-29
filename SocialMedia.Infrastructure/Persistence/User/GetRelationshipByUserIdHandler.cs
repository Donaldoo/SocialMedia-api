using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Account;
using SocialMedia.Application.Common;

namespace SocialMedia.Infrastructure.Persistence.User;

internal sealed class GetRelationshipByUserIdHandler : IRequestHandler<GetRelationshipByUserId, Guid>
{
    private readonly AppDbContext _db;
    private readonly ICurrentUser _currentUser;

    public GetRelationshipByUserIdHandler(AppDbContext db, ICurrentUser currentUser)
    {
        _db = db;
        _currentUser = currentUser;
    }

    public async Task<Guid> Handle(GetRelationshipByUserId request, CancellationToken cancellationToken)
    {
        return await _db.Relationships.Where(r => r.FollowerUserId == _currentUser.UserId && r.FollowedUserId == request.UserId)
            .Select(r => r.Id)
            .FirstOrDefaultAsync(cancellationToken);
    }
}