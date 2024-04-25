using StarLs.Core.Entities;

namespace StarLs.Tests.Builders.Entities;
public static class MockMovies
{
    public static List<Movie> Builder()
    {
        var list = new List<Movie>
        {
            new Movie(1,
                "Star Wars: A New Hope", 4,
                "It is a period of civil war. Rebel spaceships, striking from a hidden base, have won their first victory against the evil Galactic Empire...",
                "George Lucas", "Gary Kurtz, George Lucas", "25 de maio de 1977",
                MockCharacter.Builder(), MockPlanet.Builder(), MockVehicle.Builder(), MockStarship.Builder()),

            new Movie(2,
                "Star Wars: The Empire Strikes Back", 5,
                "It is a dark time for the Rebellion. Although the Death Star has been destroyed, Imperial troops have driven the Rebel forces from their hidden base and pursued them across the galaxy...",
                "Irvin Kershner", "Gary Kurtz", "20 de junho de 1980",
                MockCharacter.Builder(), MockPlanet.Builder(), MockVehicle.Builder(), MockStarship.Builder()),

            new Movie(3,
                "Star Wars: Return of the Jedi", 6,
                "Luke Skywalker has returned to his home planet of Tatooine in an attempt to rescue his friend Han Solo from the clutches of the vile gangster Jabba the Hutt...",
                "Richard Marquand", "Howard G. Kazanjian, George Lucas", "25 de maio de 1983", 
                MockCharacter.Builder(), MockPlanet.Builder(), MockVehicle.Builder(), MockStarship.Builder())
        };

        return list;
    }
}
