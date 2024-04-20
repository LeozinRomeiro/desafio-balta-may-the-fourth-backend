namespace StarLs.Core.Entities
{
    public class Starship : Entity
    {
        public string Name { get; private set; } = null !;
        public string Model { get; private set; } = null !;
        public string Manufacturer { get; private set; } = null !;
        public string CostInCredits { get; private set; } = null !;
        public string Length { get; private set; } = null !;
        public string MaxSpeed { get; private set; } = null !;
        public string Crew { get; private set; } = null !;
        public string Passengers { get; private set; } = null !;
        public string CargoCapacity { get; private set; } = null !;
        public string HyperdriveRating { get; private set; } = null !;
        public string Mglt { get; private set; } = null !;
        public string Consumables { get; private set; } = null !;
        public string Class { get; private set; } = null !;
        public List<Movie> Movies { get; private set; } = null !;
    }
}
