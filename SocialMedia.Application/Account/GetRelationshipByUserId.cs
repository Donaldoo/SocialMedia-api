using MediatR;

namespace SocialMedia.Application.Account;

public record GetRelationshipByUserId(Guid UserId): IRequest<Guid>;