using MediatR;

namespace SocialMedia.Application.Posts;

public record GetCommentByPostAndUserIdQuery(Guid PostId) : IRequest<Guid>;