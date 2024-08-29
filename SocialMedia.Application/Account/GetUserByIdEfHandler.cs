using MediatR;
using SocialMedia.Application.Common.Data;
using SocialMedia.Domain.Account;

namespace SocialMedia.Application.Account;

internal sealed class GetUserByIdEfHandler : IRequestHandler<GetUserById, User>
{
    private readonly IGenericQuery _genericQuery;

    public GetUserByIdEfHandler(IGenericQuery genericQuery)
    {
        _genericQuery = genericQuery;
    }

    public async Task<User> Handle(GetUserById request, CancellationToken cancellationToken)
    {
        return await _genericQuery.GetByIdAsync<User>(request.UserId);
    }
}