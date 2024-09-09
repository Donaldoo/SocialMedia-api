using Bogus;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Common.Dates;
using SocialMedia.Domain.Posts;

namespace SocialMedia.Infrastructure.Persistence.Common.Data.Common;

// [EfSeeder]
public static class PostSeeder
{
    public static async Task Run(AppDbContext db, IDateTimeFactory dateTimeFactory)
    {
        var users = await db.Users.ToListAsync();
        if (!users.Any())
        {
            Console.WriteLine("No users found in the database. Please seed users first.");
            return;
        }

        // Create a Faker object for generating realistic data
        var postFaker = new Faker<Post>()
            .RuleFor(p => p.Description, f => f.Lorem.Sentence())
            // .RuleFor(p => p.Image, f => $"https://picsum.photos/seed/{Guid.NewGuid()}/800/600")
            .RuleFor(p => p.CreatedAt, f => f.Date.PastOffset(1).ToUniversalTime())
            .RuleFor(p => p.Category, f => f.PickRandom<PostCategory>())
            .RuleFor(p => p.UserId, f => f.PickRandom(users).Id)
            .RuleFor(p => p.WorkExperience, (f, p) => p.Category == PostCategory.Work ? f.PickRandom<WorkExperience>() : null)
            .RuleFor(p => p.WorkIndustry, (f, p) => p.Category == PostCategory.Work ? f.PickRandom<WorkIndustry>() : null);

        var posts = postFaker.Generate(100);

        await db.Posts.AddRangeAsync(posts);
        await db.SaveChangesAsync();
    }
}