using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SocialMedia.Application.Common;
using SocialMedia.Application.Common.Data;
using SocialMedia.Infrastructure.Persistence;
using SocialMedia.Infrastructure.Persistence.Common.Data;

namespace SocialMedia.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ISettings settings)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(settings.ConnectionString, x => { x.MigrationsAssembly(AboutMe.Assembly.FullName); })
                .EnableSensitiveDataLogging()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
        
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });

        services.AddScoped<IDataWriter, EfDataWriter>();
        services.AddScoped<IGenericQuery, EfGenericQuery>();
        services.AddScoped<IDataSynchronizer, EfDataSynchronizer>();
        services.AddScoped<IDataMigrator, EfDataMigrator>();
        services.AddHttpClient();
        return services;
    }
}

public static class InfrastructureServiceProviderExtensions
{
    public static AppDbContext GetDb(this ServiceProvider serviceProvider) =>
        serviceProvider.GetService<AppDbContext>();
}