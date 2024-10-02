using Microsoft.EntityFrameworkCore;

namespace TaskManagement.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> User { get; set; }
    public DbSet<Task> Task { get; set; }
    public DbSet<Comment> Comment { get; set; }
    public DbSet<Project> Project { get; set; }


    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
