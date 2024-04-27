namespace StarLs.Core.Entities
{
    public class Vehicle : Entity
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
        public string Consumables { get; private set; } = null !;
        public string Class { get; private set; } = null !;
        public List<Movie> Movies { get; private set; } = null !;

        public Vehicle(short id, string name, string model, string manufacturer, string costInCredits, string length, 
            string maxSpeed, string crew, string passengers, string cargoCapacity, string consumables, 
            string @class)
        {
            Id = id;
            Name = name;
            Model = model;
            Manufacturer = manufacturer;
            CostInCredits = costInCredits;
            Length = length;
            MaxSpeed = maxSpeed;
            Crew = crew;
            Passengers = passengers;
            CargoCapacity = cargoCapacity;
            Consumables = consumables;
            Class = @class;
        }
    }
}
