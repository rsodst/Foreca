using Foreca.Domain;
using Microsoft.EntityFrameworkCore;

namespace Foreca.Infrastructure.Database.Database;

public class ForecaContext : DbContext
{
    public ForecaContext(DbContextOptions<ForecaContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<CityDetails> CityDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CityDetails>(builder => { builder.HasIndex(p => p.Name); });
    }
}