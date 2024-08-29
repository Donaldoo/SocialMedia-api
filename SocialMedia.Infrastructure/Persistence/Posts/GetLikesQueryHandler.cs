using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Common;
using SocialMedia.Application.Common.Dates;
using SocialMedia.Application.Posts;

namespace SocialMedia.Infrastructure.Persistence.Posts;

internal sealed class GetLikesQueryHandler: IRequestHandler<GetLikesQuery, IList<LikeDto>>
{
    private readonly AppDbContext _db;

    public GetLikesQueryHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IList<LikeDto>> Handle(GetLikesQuery request, CancellationToken cancellationToken)
    {
        return await _db.Likes
            .Where(c => c.LikedPostId == request.PostId)
            .Select(c => new LikeDto()
            {
                LikeId = c.Id,
                UserId = c.LikeUserId,
                CreatedAt = c.CreatedAt,
            })
            .ToListAsync(cancellationToken);
    }
}