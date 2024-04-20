using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarLs.Core.Entities;

namespace StarLs.Infra.Mapping;
public class PlanetMapping : IEntityTypeConfiguration<Planet>
{
    public void Configure(EntityTypeBuilder<Planet> builder)
    {
        builder.ToTable("Planet");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("VARCHAR(100)");

        builder.Property(x => x.RotationPeriod)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("VARCHAR(100)");

        builder.Property(x => x.OrbitalPeriod)
            .IsRequired()
            .HasColumnName("OrbitalPeriod")
            .HasColumnType("VARCHAR(100)");

        builder.Property(x => x.Diameter)
            .IsRequired()
            .HasColumnName("Diameter")
            .HasColumnType("VARCHAR(100)");

        builder.Property(x => x.Climate)
            .IsRequired()
            .HasColumnName("Climate")
            .HasColumnType("VARCHAR(100)");

        builder.Property(x => x.Gravity)
            .IsRequired()
            .HasColumnName("Graviry")
            .HasColumnType("VARCHAR(100)");

        builder.Property(x => x.Terrain)
            .IsRequired()
            .HasColumnName("Terrain")
            .HasColumnType("VARCHAR(100)");

        builder.Property(x => x.SurfaceWater)
            .IsRequired()
            .HasColumnName("SurfaceWater")
            .HasColumnType("VARCHAR(100)");

        builder.Property(x => x.Population)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("VARCHAR(100)");

        //builder.HasMany(p => p.Characters)
        //    .WithOne(c => c.Planet)
        //    .HasConstraintName("Fk_Planet_Character")
        //    .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(m => m.Movies)
            .WithMany(p => p.Planets);
    }
}
