using MediatR;
using SocialMedia.Application.Account;

namespace SocialMedia.Application.ChatHub;

public record GetChatParticipantsQuery(Guid ChatId) : IRequest<IList<UserDto>>;