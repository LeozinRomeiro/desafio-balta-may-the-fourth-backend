using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarLs.Core.Entities;

namespace StarLs.Infra.Mapping;
public class CharacterMapping : IEntityTypeConfiguration<Character>
{
    public void Configure(EntityTypeBuilder<Character> builder)
    {
        builder.ToTable("Character");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("VARCHAR(100)");

        builder.Property(x => x.Height)
            .IsRequired()
            .HasColumnName("Height")
            .HasColumnType("VARCHAR(20)");

        builder.Property(x => x.Weight)
            .IsRequired()
            .HasColumnName("Weight")
            .HasColumnType("VARCHAR(20)");

        builder.Property(x => x.HairColor)
            .IsRequired()
            .HasColumnName("HairColor")
            .HasColumnType("VARCHAR(20)");

        builder.Property(x => x.SkinColor)
            .IsRequired()
            .HasColumnName("SkinColor")
            .HasColumnType("VARCHAR(20)");

        builder.Property(x => x.EyeColor)
            .IsRequired()
            .HasColumnName("EyeColor")
            .HasColumnType("VARCHAR(20)");

        builder.Property(x => x.BirthYear)
            .IsRequired()
            .HasColumnName("BirthYear")
            .HasColumnType("VARCHAR(20)");

        builder.Property(x => x.Gender)
            .IsRequired()
            .HasColumnName("Gender")
            .HasColumnType("VARCHAR(20)");

        builder
            .HasOne(c => c.Planet)
            .WithMany(p => p.Characters)
            .HasForeignKey(c => c.PlanetId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
