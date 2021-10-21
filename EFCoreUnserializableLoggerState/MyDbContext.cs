using Microsoft.EntityFrameworkCore;

namespace EFCoreUnserializableLoggerState;

public class MyDbContext
    : DbContext
{
    public MyDbContext() { }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=EFCoreUnserializableLoggerState;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MyParentEntity>();
        modelBuilder.Entity<MyChildEntity>();
    }
}
