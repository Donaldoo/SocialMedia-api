using MediatR;

namespace SocialMedia.Application.Account;

public record GetOnlineUsers : IRequest<IList<UserDto>>;