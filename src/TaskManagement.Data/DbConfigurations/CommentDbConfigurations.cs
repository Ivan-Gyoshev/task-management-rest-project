using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskManagement.Data.DbConfigurations;

internal sealed class CommentDbConfigurations : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> entity)
    {
        entity.HasKey(c => c.Id);

        entity.Property(c => c.Content)
            .IsRequired()
            .HasMaxLength(500);

        entity.Property(c => c.Timestamp)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnAdd();
    }
}
