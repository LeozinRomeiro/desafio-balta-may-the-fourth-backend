
using StarLs.Core.Entities;

namespace StarLs.Tests.Builders.Entities;
public static class MockVehicle
{
    public static List<Vehicle> Builder()
    {
        var list = new List<Vehicle>
        {
            new Vehicle(1, "Landspeeder", "X-34 landspeeder", "SoroSuub Corporation", 
            "10550", "3.4", "250", "1", "1", "5", "unknown", "Repulsorcraft"),

            new Vehicle(2, "Snowspeeder", "T-47 airspeeder", "Incom Corporation", 
                "unknown", "4.5", "650", "2", "0", "10", "none", "airspeeder"),

            new Vehicle(3, "AT-AT", "All Terrain Armored Transport", 
                "Kuat Drive Yards, Imperial Department of Military Research", "unknown", 
                "20", "60", "5", "40", "1000", "unknown", "assault walker")
        };

        return list;
    }
}
