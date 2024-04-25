using StarLs.Core.Entities;

namespace StarLs.Tests.Builders.Entities;
public static class MockStarship
{
    public static List<Starship> Builder()
    {
        var list = new List<Starship>
        {
            new Starship(1, "Millennium Falcon","YT-1300 light freighter", "Corellian Engineering Corporation",
                       "100000", "34.37", "1050", "4", "6", "100000", "0.5",
                       "75", "2 months", "Light freighter", MockMovies.Builder()),

            new Starship(2, "X-wing starfighter", "T-65 X-wing", "Incom Corporation",
                        "149999", "12.5", "1050", "1", "0", "110", "1.0", "100", 
                        "1 week", "Starfighter", MockMovies.Builder()),

            new Starship(3, "TIE/ln starfighter", "Twin Ion Engine/Ln starfighter",
                        "Sienar Fleet Systems", "unknown", "7.2", "1200", "1", "0", "65", "unknown",
                        "100", "2 days", "Starfighter", MockMovies.Builder())
        };

        return list;
    }
}
