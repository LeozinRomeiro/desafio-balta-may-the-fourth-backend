
using Microsoft.EntityFrameworkCore;
using StarLs.Core.Entities;
using StarLs.Infra.Mapping;

namespace StarLs.Infra.Context;
public class AppDbContext : DbContext
{
    public DbSet<Movie> Movies { get; private set; }
    public DbSet<Planet> Planets { get; private set; }
    public DbSet<Starship> Starships { get; private set; }
    public DbSet<Vehicle> Vehicles { get; private set; }
    public DbSet<Character> Characters { get; private set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {   
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder
           .ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        modelBuilder.ApplyConfiguration(new CharacterMapping());
        modelBuilder.ApplyConfiguration(new MovieMapping());
        modelBuilder.ApplyConfiguration(new PlanetMapping());
        modelBuilder.ApplyConfiguration(new StarshipMapping());
        modelBuilder.ApplyConfiguration(new VehicleMapping());
    }

}
