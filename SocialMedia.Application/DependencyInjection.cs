using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SocialMedia.Application.Account;
using SocialMedia.Application.ChatHub;
using SocialMedia.Application.Common;
using SocialMedia.Application.Common.Behaviours;
using SocialMedia.Application.Common.Dates;
using SocialMedia.Application.Internationalization;
using SocialMedia.Application.NotificationHub;
using TicketSystem.Domain.Common.Attributes;

namespace SocialMedia.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, ISettings settings)
    {
        services.AddLogging();
        services.AddValidation();
        
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddOpenBehavior(typeof(RequestPerformanceBehaviour<,>));
            cfg.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
            cfg.AddOpenBehavior(typeof(AuthenticateBehaviour<,>));
        });
        services.AddSingleton<ILanguageResource, EnglishLanguageResource>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddScoped<IDateTimeFactory, DateTimeFactory>();
        services.AddScoped<IDateFormatter, DateFormatter>();
        services.AddScoped<IFileStorageService, FileStorageService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IChatService, ChatService>();
        services.AddSingleton(settings);

        return services;
    }
    
    private static IServiceCollection AddValidation(this IServiceCollection services)
    {
        var excludedTypes =
            AttributesUtils.GetTypesWithAttribute(Assembly.GetExecutingAssembly(), typeof(ExcludeFromDependency));

        AssemblyScanner
            .FindValidatorsInAssembly(Assembly.GetExecutingAssembly())
            .ForEach(result =>
            {
                if (!excludedTypes.Contains(result.ValidatorType))
                    services.AddScoped(result.InterfaceType, result.ValidatorType);
            });

        return services;
    }
}