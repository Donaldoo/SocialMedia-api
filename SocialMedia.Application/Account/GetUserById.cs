using MediatR;
using SocialMedia.Domain.Account;

namespace SocialMedia.Application.Account;

public record GetUserById(Guid UserId) : IRequest<User>;