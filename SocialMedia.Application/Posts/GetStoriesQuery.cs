using MediatR;

namespace SocialMedia.Application.Posts;

public record GetStoriesQuery : IRequest<IList<StoryDto>>;

public record StoryDto {
    public Guid Id { get; init; }
    public string Image { get; init; }
    public string FullName { get; init; }
    public Guid UserId { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
}