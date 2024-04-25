using StarLs.Core.Entities;

namespace StarLs.Tests.Builders.Entities;
public static class MockPlanet
{
    public static List<Planet> Builder()
    {
        var list = new List<Planet>
        {
            new Planet(1, "Tatooine", "23", "304", "10465", "Arid", 
            "1 standard", "Desert", "1", "200000", MockCharacter.Builder(), MockMovies.Builder()),

            new Planet(2, "Alderaan", "24", "364", "12500", "Temperate", 
            "1 standard", "Grasslands, Mountains", "40", "2000000000", MockCharacter.Builder(), MockMovies.Builder()),

            new Planet(3, "Dagobah", "23", "341", "8900", "Murky", 
            "N/A", "Swamp, Jungles", "8", "unknown", MockCharacter.Builder(), MockMovies.Builder())
        };

        return list;
    }
}
