using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Common;
using SocialMedia.Application.Posts;

namespace SocialMedia.Infrastructure.Persistence.Posts;

internal sealed class GetCommentByPostAndUserIdQueryHandler : IRequestHandler<GetCommentByPostAndUserIdQuery, Guid>
{
    private readonly AppDbContext _db;
    private readonly ICurrentUser _currentUser;

    public GetCommentByPostAndUserIdQueryHandler(AppDbContext db, ICurrentUser currentUser)
    {
        _db = db;
        _currentUser = currentUser;
    }

    public async Task<Guid> Handle(GetCommentByPostAndUserIdQuery request, CancellationToken cancellationToken)
    {
        return await _db.Comments.Where(l => l.PostId == request.PostId && l.CommentUserId == _currentUser.UserId).Select(l => l.Id).FirstOrDefaultAsync(cancellationToken);
    }
}