using Backend.Tests.EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Tests.EFCore;

/// <summary>
/// EF Core DbContext for testing purposes only.
/// Used to verify SQL queries by comparing ADO.NET results with EF Core LINQ results.
/// </summary>
public class PopulationDbContext : DbContext
{
    public DbSet<Country> Countries { get; set; } = null!;
    public DbSet<State> States { get; set; } = null!;
    public DbSet<City> Cities { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=citystatecountry.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId);
            entity.ToTable("Country");
            entity.Property(e => e.CountryName).IsRequired();
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.StateId);
            entity.ToTable("State");
            entity.Property(e => e.StateName).IsRequired();
            entity.HasOne(s => s.Country)
                .WithMany(c => c.States)
                .HasForeignKey(s => s.CountryId);
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId);
            entity.ToTable("City");
            entity.Property(e => e.CityName).IsRequired();
            entity.Property(e => e.Population).HasColumnName("Population");
            entity.HasOne(c => c.State)
                .WithMany(s => s.Cities)
                .HasForeignKey(c => c.StateId);
        });
    }
}
