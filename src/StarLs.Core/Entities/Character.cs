namespace StarLs.Core.Entities
{
    public class Character : Entity
    {
        public string Name { get; private set; } = null !;
        public string Height { get; private set; } = null !;
        public string Weight { get; private set; } = null !;
        public string HairColor { get; private set; } = null !;
        public string SkinColor { get; private set; } = null !;
        public string EyeColor { get; private set; } = null !;
        public string BirthYear { get; private set; } = null !;
        public string Gender { get; private set; } = null !;
        public short PlanetId { get; set; }
        public Planet Planet { get; private set; } = null !;
        public List<Movie> Movies { get; private set; } = null !;

        public Character(short id, string name, string height, string weight, string hairColor,
                        string skinColor, string eyeColor, string birthYear, string gender,
                        short planetId, Planet planet, List<Movie> movies)
        {
            Id = id;
            Name = name;
            Height = height;
            Weight = weight;
            HairColor = hairColor;
            SkinColor = skinColor;
            EyeColor = eyeColor;
            BirthYear = birthYear;
            Gender = gender;
            PlanetId = planetId;
            Planet = planet;
            Movies = movies;
        }
    }
}
