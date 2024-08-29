using MediatR;

namespace SocialMedia.Application.Posts;

public record GetLikeByPostAndUserIdQuery(Guid PostId) : IRequest<Guid>;