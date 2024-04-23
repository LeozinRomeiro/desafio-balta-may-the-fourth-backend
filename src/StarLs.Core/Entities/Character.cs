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
    }
}
