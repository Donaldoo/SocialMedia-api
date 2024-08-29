using MediatR;
using SocialMedia.Application.Common.Requests;

namespace SocialMedia.Application.Posts.DeleteLike;

public record DeleteLikeCommand(Guid PostId) : IRequest<bool>,IAuthenticatedRequest;