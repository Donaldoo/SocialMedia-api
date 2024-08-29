using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SocialMedia.Domain.ChatHub;
using SocialMedia.Domain.Posts;

namespace SocialMedia.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    private readonly ILoggerFactory _loggerFactory;
    
    public DbSet<Domain.Account.User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Relationship> Relationships { get; set; }
    public DbSet<Story> Stories { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<ChatUser> ChatUsers { get; set; }
    
    public AppDbContext(DbContextOptions options, ILoggerFactory loggerFactory) : base(options)
    {
        _loggerFactory = loggerFactory;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseLoggerFactory(_loggerFactory);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        modelBuilder.HasDbFunction(
                typeof(DbCustomFunctions).GetMethod(nameof(DbCustomFunctions.DateTrunc),
                    new[] { typeof(string), typeof(DateTime), })!)
            .HasName("date_trunc");

        modelBuilder.HasDbFunction(
                typeof(DbCustomFunctions).GetMethod(nameof(DbCustomFunctions.DateTrunc),
                    new[] { typeof(string), typeof(DateTimeOffset), })!)
            .HasName("date_trunc");

        modelBuilder.HasDbFunction(
                typeof(DbCustomFunctions).GetMethod(nameof(DbCustomFunctions.TimeZone),
                    new[] { typeof(string), typeof(DateTimeOffset), })!)
            .HasName("timezone");
    }
}

public static class DbCustomFunctions
{
    public static DateTime DateTrunc(string type, DateTime date)
        => throw new NotSupportedException();

    public static DateTime DateTrunc(string type, DateTimeOffset date)
        => throw new NotSupportedException();

    public static DateTime TimeZone(string timezone, DateTimeOffset date)
        => throw new NotSupportedException();
}

public class DateTruncTypes
{
    public const string Day = "day";
    public const string Hour = "hour";
    public const string Month = "month";

    public const string Year = "year";
    //More fields types be found https://sqlserverguides.com/postgresql-date_trunc-function/
}