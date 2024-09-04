using MediatR;

namespace SocialMedia.Application.Posts;

public record GetPostByIdQuery(Guid PostId): IRequest<PostDto>;