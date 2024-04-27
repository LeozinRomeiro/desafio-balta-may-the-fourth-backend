namespace StarLs.Core.Entities
{
    public class Movie : Entity
    {
        public string Title { get; private set; } = null !;
        public short Episode { get; private set; }
        public string OpeningCrawl { get; private set; } = null !;
        public string Director { get; private set; } = null !;
        public string Producer { get; private set; } = null !;
        public string ReleaseDate { get; private set; } = null !;
        public List<Character> Characters { get; private set; } = null !;
        public List<Planet> Planets { get; private set; } = null !;
        public List<Vehicle> Vehicles { get; private set; } = null !;
        public List<Starship> Starships { get; private set; } = null !;

        public Movie(short id, string title, short episode, string openingCrawl, string director, string producer, 
            string releaseDate)
        {
            Id = id;
            Title = title;
            Episode = episode;
            OpeningCrawl = openingCrawl;
            Director = director;
            Producer = producer;
            ReleaseDate = releaseDate;
        }
    }
}
