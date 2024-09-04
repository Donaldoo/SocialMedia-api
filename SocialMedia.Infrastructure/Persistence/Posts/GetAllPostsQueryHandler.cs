using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Posts;

namespace SocialMedia.Infrastructure.Persistence.Posts;

internal sealed class GetAllPostsQueryHandler : IRequestHandler<GetAllPosts, IList<PostDto>>
{
    private readonly AppDbContext _db;

    public GetAllPostsQueryHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IList<PostDto>> Handle(GetAllPosts request, CancellationToken cancellationToken)
    {
        return await _db.Posts
            .Where(p =>
                (!request.Category.HasValue || p.Category == request.Category) &&
                (!request.UserId.HasValue || p.UserId == request.UserId) &&
                (!request.WorkIndustry.HasValue || p.WorkIndustry == request.WorkIndustry) &&
                (!request.WorkExperience.HasValue || p.WorkExperience == request.WorkExperience)
            )
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