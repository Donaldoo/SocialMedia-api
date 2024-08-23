using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Account.Authenticator;

namespace SocialMedia.Infrastructure.Persistence.User.Auth;

internal sealed class GetAuthenticationInfoQueryEfHandler : IRequestHandler<GetAuthenticationInfoQuery, AuthenticationInfoResponse>
{
    private readonly AppDbContext _db;

    public GetAuthenticationInfoQueryEfHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<AuthenticationInfoResponse> Handle(GetAuthenticationInfoQuery request,
        CancellationToken cancellationToken)
    {
         return await _db.Users.Where(x => x.Id == request.UserId)
            .Select(x =>
                new AuthenticationInfoResponse
                {
                    UserId = x.Id,
                    Name = x.Fullname
                })
            .FirstOrDefaultAsync(cancellationToken);
    }
}