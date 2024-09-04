using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Posts;

namespace SocialMedia.Infrastructure.Persistence.Posts;

internal sealed class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, PostDto>
{
    private readonly AppDbContext _db;

    public GetPostByIdQueryHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<PostDto> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        return await _db.Posts.Where(p => p.Id == request.PostId).Select(p => new PostDto
        {
            Id = p.Id,
            Description = p.Description,
            Image = p.Image,
            CreatedAt = p.CreatedAt,
            ProfilePicture = p.User.ProfilePicture,
            FullName = p.User.FirstName + " " + p.User.LastName,
            UserId = p.UserId
        }).FirstOrDefaultAsync(cancellationToken);
    }
}