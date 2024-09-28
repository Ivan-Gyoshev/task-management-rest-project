using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskManagement.Data.DbConfigurations;

internal sealed class ProjectDbConfigurations : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> entity)
    {
        entity.HasKey(p => p.Id);

        entity.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        entity.Property(p => p.Description)
            .HasMaxLength(500);

        entity.Property(p => p.Timestamp)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnAddOrUpdate();
    }
}
