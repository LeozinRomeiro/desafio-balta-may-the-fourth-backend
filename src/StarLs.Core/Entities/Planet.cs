namespace StarLs.Core.Entities
{
    public class Planet : Entity
    {
        public string Title { get; private set; } = null !;
        public string RotationPeriod { get; private set; } = null !;
        public string OrbitalPeriod { get; private set; } = null !;
        public string Diameter { get; private set; } = null !;
        public string Climate { get; private set; } = null !;
        public string Gravity { get; private set; } = null !;
        public string Terrain { get; private set; } = null !;
        public string SurfaceWater { get; private set; } = null !;
        public string Population { get; private set; } = null !;
        public List<Character> Characters { get; private set; } = null !;
        public List<Movie> Movies { get; private set; } = null !;

        public Planet(short id, string title, string rotationPeriod, string orbitalPeriod, string diameter, string climate, 
            string gravity, string terrain, string surfaceWater, string population)
        {
            Id = id;
            Title = title;
            RotationPeriod = rotationPeriod;
            OrbitalPeriod = orbitalPeriod;
            Diameter = diameter;
            Climate = climate;
            Gravity = gravity;
            Terrain = terrain;
            SurfaceWater = surfaceWater;
            Population = population;
        }
    }
}
