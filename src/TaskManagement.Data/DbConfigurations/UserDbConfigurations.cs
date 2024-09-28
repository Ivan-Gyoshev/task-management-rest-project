using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskManagement.Data.DbConfigurations;

internal sealed class UserDbConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.HasKey(u => u.Id);

        entity.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(100);

        entity.Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(255);
    }
}
