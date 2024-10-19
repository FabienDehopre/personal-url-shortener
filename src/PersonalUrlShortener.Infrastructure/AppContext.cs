using Microsoft.EntityFrameworkCore;
using PersonalUrlShortener.Core.Entities;

namespace PersonalUrlShortener.Infrastructure;

public class AppContext : DbContext
{
    public AppContext(DbContextOptions<AppContext> options) : base(options)
    {
    }
    
    public DbSet<Link> Links { get; set; }
    public DbSet<Visit> Visits { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppContext).Assembly);
    }
}