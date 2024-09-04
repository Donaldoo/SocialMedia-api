using System.Security.Claims;
using System.Text;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SocialMedia.Api.Auth;
using SocialMedia.Api.Common.Extensions;
using SocialMedia.Api.Common.MiddleWares;
using SocialMedia.Api.Endpoints;
using SocialMedia.Application;
using SocialMedia.Application.Common;
using SocialMedia.Application.Common.Data;
using SocialMedia.Application.NotificationHub;
using SocialMedia.Infrastructure;
using SocialMedia.Infrastructure.Persistence.ChatHub;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

var settings = Settings.GetSettingsFromEnvironment();

builder.Services
    .AddApplication(settings)
    .AddInfrastructure(settings);

builder.Services.AddScoped<ICurrentUser>(opts =>
{
    var accessor = opts.GetRequiredService<IHttpContextAccessor>();
    if (accessor.HttpContext?.User.Identity != null && accessor.HttpContext != null &&
        accessor.HttpContext.User.Identity.IsAuthenticated)
    {
        return new CurrentUser
        {
            UserId = accessor.HttpContext.User.GetUserAuthenticationInfo().UserId,
        };
    }

    return null;
});

builder.Services.AddScoped<ITokenGenerator, JwtTokenGenerator>();

builder.Services.AddApiVersioning(x =>
{
    x.DefaultApiVersion = new ApiVersion(1.0);
    x.AssumeDefaultVersionWhenUnspecified = true;
    x.ReportApiVersions = true;
    x.ApiVersionReader = new MediaTypeApiVersionReader("api-version");
}).AddApiExplorer();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen((opts) =>
{
    opts.CustomSchemaIds(c => c.FullName.Replace('+', '.'));
    opts.SwaggerDoc("v1", new OpenApiInfo { Title = "Social Media", Version = "v1" });
    opts.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    opts.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    opts.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder.WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("asddnasdnasjdioqwurewqoyreqw8798!23312qweqwew")),
            ValidateIssuer = false,
            ValidateAudience = false,
            NameClaimType = ClaimTypes.NameIdentifier,
            RoleClaimType = ClaimTypes.Role,
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];
                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) &&
                    (path.StartsWithSegments("/notificationHub") || path.StartsWithSegments("/chathub")))
                {
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddSignalR();

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 10 * 1024 * 1024; // 10 MB
});


var app = builder.Build();

JsonConvert.DefaultSettings = () => new JsonSerializerSettings
{
    ContractResolver = new CamelCasePropertyNamesContractResolver(),
};

using var scope = app.Services.CreateScope();

scope.ServiceProvider.GetRequiredService<IDataMigrator>().Migrate();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Social Media"); });
}

app.UseStaticFiles();
app.UseCors();

app.UseAppHealthCheck();
app.UseAuthentication();
app.UseAuthorization();
app.UseGenericErrorHandling();
app.UsePreventClickJacking();
app.MapHub<ChatHub>("/chathub");
app.MapHub<NotificationHub>("/notificationHub");
app.MapApiEndpoints();

app.Run();