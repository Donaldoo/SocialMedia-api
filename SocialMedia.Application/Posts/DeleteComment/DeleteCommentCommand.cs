using MediatR;
using SocialMedia.Application.Common.Requests;

namespace SocialMedia.Application.Posts.DeleteComment;

public record DeleteCommentCommand(Guid CommentId) : IRequest<bool>, IAuthenticatedRequest;