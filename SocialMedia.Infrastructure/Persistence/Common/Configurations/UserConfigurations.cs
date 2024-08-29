using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SocialMedia.Infrastructure.Persistence.Common.Configurations;

public class UserConfigurations : BaseConfigurations, IEntityTypeConfiguration<Domain.Account.User>
{
    public void Configure(EntityTypeBuilder<Domain.Account.User> builder)
    {
        builder.ToTable(nameof(User)).HasKey(c => c.Id);
        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.LastName).IsRequired();
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.Password).IsRequired();
    }
}