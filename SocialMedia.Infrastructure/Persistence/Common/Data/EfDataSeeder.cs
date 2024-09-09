using System.Reflection;
using SocialMedia.Application.Account;
using SocialMedia.Application.Common.Data;
using SocialMedia.Application.Common.Dates;
using SocialMedia.Infrastructure.Persistence.Common.Data.Common;

namespace SocialMedia.Infrastructure.Persistence.Common.Data;

public class EfDataSeeder : IDataSeeder
{
    private const string SeederMethodName = "Run";

    private readonly AppDbContext _db;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IDateTimeFactory _dateTimeFactory;

    public EfDataSeeder(AppDbContext db, IPasswordHasher passwordHasher, IDateTimeFactory dateTimeFactory)
    {
        _db = db;
        _passwordHasher = passwordHasher;
        _dateTimeFactory = dateTimeFactory;
    }

    public async Task SeedAsync()
    {
        var seeders = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Select(
                t => new
                {
                    Class = t,
                    SeederAttribute = t.GetCustomAttribute<EfSeederAttribute>(true),
                    RunMethod = t.GetMethod(SeederMethodName, BindingFlags.Public | BindingFlags.Static)
                }
            ).Where(t => t.SeederAttribute != null && t.RunMethod != null);

        foreach (var seeder in seeders)
        {
            if (seeder.SeederAttribute!.Enabled)
            {
                await (Task) seeder.RunMethod!.Invoke(null, GenerateParamSet(seeder.RunMethod))!;
            }
        }
    }

    private object[] GenerateParamSet(MethodInfo runMethod)
    {
        List<object> parameters = new();

        foreach (var parameter in runMethod.GetParameters())
        {
            if (parameter.ParameterType == typeof(AppDbContext))
            {
                parameters.Add(_db);
            }
            else if (parameter.ParameterType == typeof(IPasswordHasher))
            {
                parameters.Add(_passwordHasher);
            }
            else if (parameter.ParameterType == typeof(IDateTimeFactory))
            {
                parameters.Add(_dateTimeFactory);
            }
           
        }
        return parameters.ToArray();
    }
}