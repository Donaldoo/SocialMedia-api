using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Account;
using SocialMedia.Application.ChatHub;

namespace SocialMedia.Infrastructure.Persistence.ChatHub;

internal sealed class GetChatParticipantsQueryHandler : IRequestHandler<GetChatParticipantsQuery, IList<UserDto>>
{
    private readonly AppDbContext _db;

    public GetChatParticipantsQueryHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IList<UserDto>> Handle(GetChatParticipantsQuery request, CancellationToken cancellationToken)
    {
        return await _db.ChatUsers.Where(cu => cu.ChatId == request.ChatId)
            .Select(cu => new UserDto
            {
                Id = cu.UserId,
                DisplayName = cu.User.FirstName + " " + cu.User.LastName,
                ProfilePicture = cu.User.ProfilePicture
            })
            .ToListAsync(cancellationToken);
    }
}