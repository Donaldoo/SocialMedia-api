using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Common;
using SocialMedia.Application.Common.Dates;
using SocialMedia.Application.Posts;

namespace SocialMedia.Infrastructure.Persistence.Posts;

internal sealed class GetCommentsQueryHandler : IRequestHandler<GetCommentsQuery, IList<CommentDto>>
{
    private readonly IDateTimeFactory _dateTimeFactory;
    private readonly ICurrentUser _currentUser;
    private readonly AppDbContext _db;

    public GetCommentsQueryHandler(IDateTimeFactory dateTimeFactory, ICurrentUser currentUser, AppDbContext db)
    {
        _dateTimeFactory = dateTimeFactory;
        _currentUser = currentUser;
        _db = db;
    }

    public async Task<IList<CommentDto>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
    {
        return await _db.Comments
            .Where(c => c.PostId == request.PostId)
            .Select(c => new CommentDto
            {
                CommentId = c.Id,
                Description = c.Description,
                CreatedAt = c.CreatedAt,
                CanBeDeleted = c.CommentUserId == _currentUser.UserId,
                UserImage = _db.Users
                    .Where(u => u.Id == c.CommentUserId)
                    .Select(u => u.ProfilePicture)
                    .FirstOrDefault(),
                UserName = _db.Users.Where(u => u.Id == c.CommentUserId).Select(u => u.FirstName + " " + u.LastName).FirstOrDefault()
            })
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}