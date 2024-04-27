using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarLs.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    Id = table.Column<short>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Episode = table.Column<short>(type: "TINYINT", nullable: false),
                    OpeningCrawl = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    Director = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Producer = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    ReleaseDate = table.Column<string>(type: "VARCHAR(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Planet",
                columns: table => new
                {
                    Id = table.Column<short>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    RotationPeriod = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    OrbitalPeriod = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Diameter = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Climate = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Gravity = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Terrain = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    SurfaceWater = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Population = table.Column<string>(type: "VARCHAR(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Starship",
                columns: table => new
                {
                    Id = table.Column<short>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Model = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Manufacturer = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    CostInCredits = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Length = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    MaxSpeed = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Crew = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Passengers = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    CargoCapacity = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    HyperdriveRating = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Mglt = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Consumables = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Class = table.Column<string>(type: "VARCHAR(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Starship", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicle",
                columns: table => new
                {
                    Id = table.Column<short>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Model = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Manufacturer = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    CostInCredits = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Length = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    MaxSpeed = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Crew = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Passengers = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    CargoCapacity = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Consumables = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Class = table.Column<string>(type: "VARCHAR(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Character",
                columns: table => new
                {
                    Id = table.Column<short>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Height = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    Weight = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    HairColor = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    SkinColor = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    EyeColor = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    BirthYear = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    Gender = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    PlanetId = table.Column<short>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Character_Planet_PlanetId",
                        column: x => x.PlanetId,
                        principalTable: "Planet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MoviePlanet",
                columns: table => new
                {
                    MoviesId = table.Column<short>(type: "INTEGER", nullable: false),
                    PlanetsId = table.Column<short>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoviePlanet", x => new { x.MoviesId, x.PlanetsId });
                    table.ForeignKey(
                        name: "FK_MoviePlanet_Movie_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoviePlanet_Planet_PlanetsId",
                        column: x => x.PlanetsId,
                        principalTable: "Planet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieStarship",
                columns: table => new
                {
                    MoviesId = table.Column<short>(type: "INTEGER", nullable: false),
                    StarshipsId = table.Column<short>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieStarship", x => new { x.MoviesId, x.StarshipsId });
                    table.ForeignKey(
                        name: "FK_MovieStarship_Movie_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieStarship_Starship_StarshipsId",
                        column: x => x.StarshipsId,
                        principalTable: "Starship",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieVehicle",
                columns: table => new
                {
                    MoviesId = table.Column<short>(type: "INTEGER", nullable: false),
                    VehiclesId = table.Column<short>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieVehicle", x => new { x.MoviesId, x.VehiclesId });
                    table.ForeignKey(
                        name: "FK_MovieVehicle_Movie_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieVehicle_Vehicle_VehiclesId",
                        column: x => x.VehiclesId,
                        principalTable: "Vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterMovie",
                columns: table => new
                {
                    CharactersId = table.Column<short>(type: "INTEGER", nullable: false),
                    MoviesId = table.Column<short>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterMovie", x => new { x.CharactersId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_CharacterMovie_Character_CharactersId",
                        column: x => x.CharactersId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterMovie_Movie_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Character_PlanetId",
                table: "Character",
                column: "PlanetId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterMovie_MoviesId",
                table: "CharacterMovie",
                column: "MoviesId");

            migrationBuilder.CreateIndex(
                name: "IX_MoviePlanet_PlanetsId",
                table: "MoviePlanet",
                column: "PlanetsId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieStarship_StarshipsId",
                table: "MovieStarship",
                column: "StarshipsId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieVehicle_VehiclesId",
                table: "MovieVehicle",
                column: "VehiclesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterMovie");

            migrationBuilder.DropTable(
                name: "MoviePlanet");

            migrationBuilder.DropTable(
                name: "MovieStarship");

            migrationBuilder.DropTable(
                name: "MovieVehicle");

            migrationBuilder.DropTable(
                name: "Character");

            migrationBuilder.DropTable(
                name: "Starship");

            migrationBuilder.DropTable(
                name: "Movie");

            migrationBuilder.DropTable(
                name: "Vehicle");

            migrationBuilder.DropTable(
                name: "Planet");
        }
    }
}
