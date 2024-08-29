using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Common;
using SocialMedia.Application.Posts;

namespace SocialMedia.Infrastructure.Persistence.Posts;

internal sealed class GetLikeByPostAndUserIdQueryHandler : IRequestHandler<GetLikeByPostAndUserIdQuery, Guid>
{
    private readonly AppDbContext _db;
    private readonly ICurrentUser _currentUser;

    public GetLikeByPostAndUserIdQueryHandler(AppDbContext db, ICurrentUser currentUser)
    {
        _db = db;
        _currentUser = currentUser;
    }

    public async Task<Guid> Handle(GetLikeByPostAndUserIdQuery request, CancellationToken cancellationToken)
    {
        return await _db.Likes.Where(l => l.LikedPostId == request.PostId && l.LikeUserId == _currentUser.UserId).Select(l => l.Id).FirstOrDefaultAsync(cancellationToken);
    }
}