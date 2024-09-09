using Bogus;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Account;
using SocialMedia.Application.Common.Dates;

namespace SocialMedia.Infrastructure.Persistence.Common.Data.Common;

// [EfSeeder]
public static class UserSeeder
{
    public static async Task Run(AppDbContext db, IPasswordHasher passwordHasher, IDateTimeFactory dateTimeFactory)
    {
        var faker = new Faker();

        for (int i = 1; i <= 100; i++)
        {
            var email = faker.Internet.Email();
            var user = await db.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                user = new Domain.Account.User
                {
                    Email = email,
                    FirstName = faker.Name.FirstName(),
                    LastName = faker.Name.LastName(),
                    Password = passwordHasher.HashPassword("123456"),
                    City = faker.Address.City(),
                    Bio = faker.Lorem.Sentence(),
                    ProfilePicture = $"https://picsum.photos/seed/{Guid.NewGuid()}/400/400",
                    CoverPicture = $"https://picsum.photos/seed/{Guid.NewGuid()}/1200/400"
                };
                await db.Users.AddAsync(user);
            }
        }

        await db.SaveChangesAsync();
    }
}