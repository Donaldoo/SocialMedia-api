using MediatR;
using SocialMedia.Domain.Account;

namespace SocialMedia.Application.Account;

public record GetUserFollowingQuery() : IRequest<IList<UserDto>>;