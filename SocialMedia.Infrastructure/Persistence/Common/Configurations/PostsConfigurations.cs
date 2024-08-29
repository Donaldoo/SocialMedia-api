using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Domain.Posts;

namespace SocialMedia.Infrastructure.Persistence.Common.Configurations;

public class PostsConfigurations : BaseConfigurations, IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable(nameof(Post)).HasKey(c => c.Id);
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.Category).IsRequired();

        builder.HasOne(o => o.User)
            .WithMany()
            .IsRequired()
            .HasForeignKey(o => o.UserId);
    }
}

public class CommentsConfigurations : BaseConfigurations, IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable(nameof(Comment)).HasKey(c => c.Id);
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.PostId).IsRequired();

        builder.HasOne(o => o.User)
            .WithMany()
            .IsRequired()
            .HasForeignKey(o => o.CommentUserId);

        builder.HasOne(o => o.Post)
            .WithMany()
            .IsRequired()
            .HasForeignKey(o => o.PostId);
    }
}

public class LikesConfigurations : BaseConfigurations, IEntityTypeConfiguration<Like>
{
    public void Configure(EntityTypeBuilder<Like> builder)
    {
        builder.ToTable(nameof(Like)).HasKey(c => c.Id);

        builder.HasOne(o => o.User)
            .WithMany()
            .IsRequired()
            .HasForeignKey(o => o.LikeUserId);

        builder.HasOne(o => o.Post)
            .WithMany()
            .IsRequired()
            .HasForeignKey(o => o.LikedPostId);
    }
}

public class StoriesConfigurations : BaseConfigurations, IEntityTypeConfiguration<Story>
{
    public void Configure(EntityTypeBuilder<Story> builder)
    {
        builder.ToTable(nameof(Story)).HasKey(c => c.Id);
        builder.Property(x => x.CreatedAt).IsRequired();

        builder.HasOne(o => o.User)
            .WithMany()
            .IsRequired()
            .HasForeignKey(o => o.StoryUserId);
    }
}

public class RelationshipsConfigurations : BaseConfigurations, IEntityTypeConfiguration<Relationship>
{
    public void Configure(EntityTypeBuilder<Relationship> builder)
    {
        builder.ToTable(nameof(Relationship)).HasKey(c => c.Id);

        builder.HasOne(o => o.FollowedUser)
            .WithMany()
            .IsRequired()
            .HasForeignKey(o => o.FollowedUserId);

        builder.HasOne(o => o.FollowerUser)
            .WithMany()
            .IsRequired()
            .HasForeignKey(o => o.FollowerUserId);
    }
}