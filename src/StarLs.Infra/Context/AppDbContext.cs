using Microsoft.EntityFrameworkCore;
using StarLs.Core.Entities;

namespace StarLs.Infra.Context;
public class AppDbContext : DbContext
{
    public DbSet<Movie> Movies { get; private set; }
    public DbSet<Planet> Planets { get; private set; }
    public DbSet<Starship> Starships { get; private set; }
    public DbSet<Vehicle> Vehicles { get; private set; }
    public DbSet<Character> Characters { get; private set; }

    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {   
    }

}
