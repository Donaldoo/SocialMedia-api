using MediatR;
using SocialMedia.Application.Common.Requests;

namespace SocialMedia.Application.Posts.DeletePost;

public record DeletePostCommand(Guid PostId) : IRequest<bool>, IAuthenticatedRequest;