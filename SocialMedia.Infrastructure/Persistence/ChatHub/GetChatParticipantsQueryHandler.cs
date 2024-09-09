using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Account;
using SocialMedia.Application.ChatHub;
using SocialMedia.Application.Common;

namespace SocialMedia.Infrastructure.Persistence.ChatHub;

internal sealed class GetChatParticipantsQueryHandler : IRequestHandler<GetChatParticipantsQuery, IList<UserDto>>
{
    private readonly AppDbContext _db;
    private readonly ICurrentUser _currentUser;

    public GetChatParticipantsQueryHandler(AppDbContext db, ICurrentUser currentUser)
    {
        _db = db;
        _currentUser = currentUser;
    }

    public async Task<IList<UserDto>> Handle(GetChatParticipantsQuery request, CancellationToken cancellationToken)
    {
        return await _db.ChatUsers.Where(cu => cu.ChatId == request.ChatId && cu.UserId != _currentUser.UserId)
            .Select(cu => new UserDto
            {
                Id = cu.UserId,
                DisplayName = cu.User.FirstName + " " + cu.User.LastName,
                ProfilePicture = cu.User.ProfilePicture
            })
            .ToListAsync(cancellationToken);
    }
}