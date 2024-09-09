
namespace SocialMedia.Infrastructure.Persistence.Common.Data.Common;

public static class EfSeederUtils
{
    public static bool SeedExistsInDb<T>(IEnumerable<T> seed, IEnumerable<T> db)
    {
        return seed.Select(db.Contains).All(exists => exists);
    }
}