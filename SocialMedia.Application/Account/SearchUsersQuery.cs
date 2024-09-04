using MediatR;
using SocialMedia.Application.Common.Requests;

namespace SocialMedia.Application.Account;

public record SearchUsersQuery(string Search) : IRequest<IList<UserDto>>, IAuthenticatedRequest;