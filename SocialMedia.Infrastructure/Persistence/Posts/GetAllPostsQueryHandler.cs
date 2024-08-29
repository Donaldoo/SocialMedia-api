using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Common;
using SocialMedia.Application.Posts;
using SocialMedia.Domain.Posts;

namespace SocialMedia.Infrastructure.Persistence.Posts;

internal sealed class GetAllPostsQueryHandler : IRequestHandler<GetAllPosts, IList<PostDto>>
{
    private readonly AppDbContext _db;
    private readonly ICurrentUser _currentUser;

    public GetAllPostsQueryHandler(AppDbContext db, ICurrentUser currentUser)
    {
        _db = db;
        _currentUser = currentUser;
    }

    public async Task<IList<PostDto>> Handle(GetAllPosts request, CancellationToken cancellationToken)
    {
        return await _db.Posts
            .Where(p =>
                !request.Category.HasValue || p.Category == request.Category).Where(p =>
                !request.UserId.HasValue || p.UserId == request.UserId).Where(p =>
                request.UserId.HasValue || _db.Relationships.Any(r =>
                    r.FollowerUserId == _currentUser.UserId && r.FollowedUserId == p.UserId) ||
                p.UserId == _currentUser.UserId)
            .Select(p => new PostDto
            {
                Id = p.Id,
                Description = p.Description,
                CreatedAt = p.CreatedAt,
                Image = p.Image,
                UserId = p.UserId,
                FullName = p.User.FirstName + " " + p.User.LastName,
                ProfilePicture = p.User.ProfilePicture
            })
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}