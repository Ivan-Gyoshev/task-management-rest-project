using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskManagement.Data.DbConfigurations;

public sealed class TaskDbConfigurations : IEntityTypeConfiguration<Task>
{
    public DbSet<Task> Tasks { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Comment> Comments { get; set; }

    public void Configure(EntityTypeBuilder<Task> entity)
    {
        entity.HasKey(t => t.Id);

        entity.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(100);

        entity.Property(t => t.Description)
            .HasMaxLength(500);

        entity.Property(t => t.Status)
            .HasConversion<int>()
            .IsRequired();

        entity.Property(t => t.DueDate)
            .IsRequired(false);

        entity.Property(t => t.Timestamp)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnAddOrUpdate();
    }
}
