using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Account.Authenticator;

namespace SocialMedia.Infrastructure.Persistence.User.Auth;

internal class GetUserByEmailQueryEfHandler : IRequestHandler<GetUserByEmailQuery, Domain.Account.User >
{
    private readonly AppDbContext _db;

    public GetUserByEmailQueryEfHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Domain.Account.User> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        if (string.IsNullOrWhiteSpace(request.Email)) throw new ArgumentNullException(nameof(request.Email));

        return await _db.Users.FirstOrDefaultAsync(c => c.Email.ToLower().Equals(request.Email.ToLower()), cancellationToken);
    }
} 