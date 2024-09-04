using SocialMedia.Domain.Account;
using SocialMedia.Domain.Common;

namespace SocialMedia.Domain.Posts;

public record Post : EntityWithGuid
{
    public string Description { get; init; }
    public string Image { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public PostCategory Category { get; init; }
    public WorkExperience? WorkExperience { get; init; }
    public WorkIndustry? WorkIndustry { get; init; }
    public Guid UserId { get; init; }
    public User User { get; init; }
}

public enum PostCategory
{
    Work = 1,
    Entertainment,
}

public enum WorkExperience
{
    FullTime = 1,
    PartTime,
    Internship,
}

public enum WorkIndustry
{
    Technology = 1,
    Finance,
    Marketing,
    Healthcare,
    Education,
    Legal,
    Engineering,
    Retail,
    Hospitality,
    Government,
}