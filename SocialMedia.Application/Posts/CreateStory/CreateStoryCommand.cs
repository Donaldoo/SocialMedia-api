using MediatR;

namespace SocialMedia.Application.Posts.CreateStory;

public record CreateStoryCommand(string Image) : IRequest<Guid>;