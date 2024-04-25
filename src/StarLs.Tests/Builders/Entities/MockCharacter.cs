﻿using StarLs.Core.Entities;

namespace StarLs.Tests.Builders.Entities;
public static class MockCharacter
{
    public static List<Character> Builder()
    {
        var list = new List<Character>
        {
            new Character(1, "Luke Skywalker", "172", "77", "Blond", "Fair", "Blue", "19BBY", "Male", 1,
            MockPlanet.Builder().Find(x => x.Id == 1)!, MockMovies.Builder()),

            new Character(2, "Leia Organa", "150", "49", "Brown", "Light", "Brown", "19BBY", "Female", 2,
            MockPlanet.Builder().Find(x => x.Id == 2)!, MockMovies.Builder()),

            new Character(3, "Han Solo", "180", "80", "Brown", "Fair", "Brown", "29BBY", "Male", 3,
            MockPlanet.Builder().Find(x => x.Id == 3)!, MockMovies.Builder()),
        };

        return list;
    }
}