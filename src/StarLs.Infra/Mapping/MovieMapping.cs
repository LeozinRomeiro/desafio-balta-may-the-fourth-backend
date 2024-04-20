using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarLs.Core.Entities;

namespace StarLs.Infra.Mapping;
public class MovieMapping : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable("Movie");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasColumnName("Title")
            .HasColumnType("VARCHAR(100)");

        builder.Property(x => x.Episode)
            .IsRequired()
            .HasColumnName("Episode")
            .HasColumnType("TINYINT");

        builder.Property(x => x.OpeningCrawl)
            .IsRequired()
            .HasColumnName("OpeningCrawl")
            .HasColumnType("VARCHAR(255)");

        builder.Property(x => x.Director)
            .IsRequired()
            .HasColumnName("Director")
            .HasColumnType("VARCHAR(100)");

        builder.Property(x => x.Producer)
            .IsRequired()
            .HasColumnName("Director")
            .HasColumnType("VARCHAR(100)");

        builder.Property(x => x.ReleaseDate)
            .IsRequired()
            .HasColumnName("ReleaseDate")
            .HasColumnType("VARCHAR(100)");


        builder.HasMany(m => m.Characters)
            .WithMany(c => c.Movies);
        //.UsingEntity(
        //"MovieCharacter",
        //    l => l.HasOne(typeof(Character))
        //        .WithMany()
        //        .HasForeignKey("CharactersId")
        //        .HasPrincipalKey(nameof(Character.Id)),
        //    r => r.HasOne(typeof(Movie))
        //        .WithMany()
        //        .HasForeignKey("MoviesId")
        //        .HasPrincipalKey(nameof(Movie.Id)),
        //    j => j.HasKey("MoviesId", "CharactersId"));

        builder.HasMany(m => m.Planets)
            .WithMany(c => c.Movies);

        builder.HasMany(m => m.Vehicles)
            .WithMany(c => c.Movies);

        builder.HasMany(m => m.Starships)
            .WithMany(c => c.Movies);
    }
}
