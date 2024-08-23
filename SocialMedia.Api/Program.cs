using Asp.Versioning;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SocialMedia.Api.Auth;
using SocialMedia.Api.Common.Extensions;
using SocialMedia.Api.Common.MiddleWares;
using SocialMedia.Api.Endpoints;
using SocialMedia.Application;
using SocialMedia.Application.Common;
using SocialMedia.Infrastructure;

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
builder.Services.Configure<HostOptions>(x =>
{
    x.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
});

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
    options.AddDefaultPolicy(policy =>
    {
        policy
            .SetIsOriginAllowedToAllowWildcardSubdomains()
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddAuthorization();
builder.Services.AddBearerAuthentication(builder.Configuration);


var app = builder.Build();

JsonConvert.DefaultSettings = () => new JsonSerializerSettings
{
    ContractResolver = new CamelCasePropertyNamesContractResolver(),
};

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Social Media"); });
}

app.UseCors();

app.UseAppHealthCheck();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UsePreventClickJacking();
app.MapApiEndpoints();

app.Run();
