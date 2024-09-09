using MediatR;
using SocialMedia.Domain.Account;

namespace SocialMedia.Application.Account;

public record GetUserById(Guid UserId) : IRequest<UserDetails>;

public record UserDetails : User
{
    public int FollowingCount { get; init; }
    public int FollowersCount { get; init; }
}