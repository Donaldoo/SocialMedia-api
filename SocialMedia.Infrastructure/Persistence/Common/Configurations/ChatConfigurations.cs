using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Domain.ChatHub;

namespace SocialMedia.Infrastructure.Persistence.Common.Configurations;

public class ChatConfigurations : BaseConfigurations, IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.ToTable(nameof(Chat)).HasKey(c => c.Id);
    }
}

public class MessageConfigurations : BaseConfigurations, IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable(nameof(Message)).HasKey(c => c.Id);
        builder.Property(x => x.Content).IsRequired();
        builder.Property(x => x.SentAt).IsRequired();

        builder.HasOne(o => o.Sender)
            .WithMany()
            .HasForeignKey(o => o.SenderId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(o => o.Chat)
            .WithMany()
            .HasForeignKey(o => o.ChatId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}

public class ChatUserConfigurations : BaseConfigurations, IEntityTypeConfiguration<ChatUser>
{
    public void Configure(EntityTypeBuilder<ChatUser> builder)
    {
        builder.ToTable(nameof(ChatUser)).HasKey(c => new { c.ChatId, c.UserId });
        
        builder.HasOne(o => o.Chat)
            .WithMany()
            .IsRequired()
            .HasForeignKey(o => o.ChatId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.User)
            .WithMany()
            .HasForeignKey(o => o.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}