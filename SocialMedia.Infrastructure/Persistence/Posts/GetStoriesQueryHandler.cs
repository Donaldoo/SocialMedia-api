using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Common;
using SocialMedia.Application.Common.Dates;
using SocialMedia.Application.Posts;

namespace SocialMedia.Infrastructure.Persistence.Posts;

internal sealed class GetStoriesQueryHandler : IRequestHandler<GetStoriesQuery, IList<StoryDto>>
{
    private readonly AppDbContext _db;
    private readonly ICurrentUser _currentUser;
    private readonly IDateTimeFactory _dateTimeFactory;

    public GetStoriesQueryHandler(AppDbContext db, IDateTimeFactory dateTimeFactory, ICurrentUser currentUser)
    {
        _db = db;
        _dateTimeFactory = dateTimeFactory;
        _currentUser = currentUser;
    }

    public async Task<IList<StoryDto>> Handle(GetStoriesQuery request, CancellationToken cancellationToken)
    {
        return await _db.Stories
            .Where(s => s.CreatedAt >= _dateTimeFactory.UtcNowWithOffset().AddDays(-1) &&
                        (_db.Relationships.Any(r =>
                            r.FollowerUserId == _currentUser.UserId &&
                            r.FollowedUserId == s.StoryUserId) || s.StoryUserId == _currentUser.UserId))
            .Select(s => new StoryDto
            {
                Id = s.Id,
                Image = s.Image,
                FullName = s.User.FirstName + " " + s.User.LastName,
                UserId = s.StoryUserId,
                CreatedAt = s.CreatedAt
            })
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}