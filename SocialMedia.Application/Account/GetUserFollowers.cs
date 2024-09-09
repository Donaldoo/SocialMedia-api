using MediatR;

namespace SocialMedia.Application.Account;

public record GetUserFollowers : IRequest<IList<UserDto>>;