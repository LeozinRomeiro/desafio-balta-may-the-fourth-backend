using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarLs.Core.Entities;
using System.Reflection.Emit;

namespace StarLs.Infra.Mapping;
public class VehicleMapping : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("Starship");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("VARCHAR(100)");

        builder.Property(x => x.Model)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("VARCHAR(100)");

        builder.Property(x => x.Manufacturer)
            .IsRequired()
            .HasColumnName("Manufacturer")
            .HasColumnType("VARCHAR(100)");

        builder.Property(x => x.CostInCredits)
            .IsRequired()
            .HasColumnName("CostInCredits")
            .HasColumnType("VARCHAR(100)");

        builder.Property(x => x.Length)
            .IsRequired()
            .HasColumnName("Length")
            .HasColumnType("VARCHAR(20)");

        builder.Property(x => x.MaxSpeed)
            .IsRequired()
            .HasColumnName("MaxSpeed")
            .HasColumnType("VARCHAR(100)");

        builder.Property(x => x.Crew)
            .IsRequired()
            .HasColumnName("Crew")
            .HasColumnType("VARCHAR(100)");

        builder.Property(x => x.Passengers)
            .IsRequired()
            .HasColumnName("Passengers")
            .HasColumnType("VARCHAR(100)");

        builder.Property(x => x.CargoCapacity)
            .IsRequired()
            .HasColumnName("CargoCapacity")
            .HasColumnType("VARCHAR(100)");

        builder.Property(x => x.Consumables)
            .IsRequired()
            .HasColumnName("Consumables")
            .HasColumnType("VARCHAR(100)");

        builder.Property(x => x.Class)
            .IsRequired()
            .HasColumnName("Class")
            .HasColumnType("VARCHAR(100)");

        builder
            .HasMany(v => v.Movies)
            .WithMany(m => m.Vehicles);
    }
}
