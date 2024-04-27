using Serilog;
using StarLs.Core.Entities;
using System.IO;
using System.Text.Json;

namespace StarLs.SeedDataBase.Request;
internal class MovieRequest : BaseRequest
{
    public Dictionary<short, List<short>> CharactersIds { get; private set; } = [];
    public Dictionary<short, List<short>> PlanetsIds { get; private set; } = [];
    public Dictionary<short, List<short>> VehiclesIds { get; private set; } = [];
    public Dictionary<short, List<short>> StarshipsIds { get; private set; } = [];

    public MovieRequest(IHttpClientFactory httpClient, ILogger logger) : base(httpClient.CreateClient("https://swapi.py4e.com/api/"), logger)
    {
        _endPoint = "films";
        _httpClient.BaseAddress = new Uri(_url);
    }

    public List<Movie> Get()
    {
        var movies = new List<Movie>();
        try
        {
            List<JsonElement> jsonList = [];
            string? nextRequest;
            do
            {
                jsonList.AddRange(MakeRequest(out nextRequest));
                _endPoint = nextRequest ?? _endPoint;
            } while (nextRequest != null);

            movies = ConvertJsonToPlanet(jsonList);
        }
        catch (Exception ex)
        {
            _logger.Error($"Comunication api fail. unhandled exception - {ex.Message}");
        }

        return movies;
    }

    private List<JsonElement> MakeRequest(out string? nextRequest)
    {
        HttpResponseMessage response = _httpClient.GetAsync($"{_endPoint}").Result;
        var jsonList = new List<JsonElement>();
        nextRequest = null;

        if (!response.IsSuccessStatusCode)
        {
            _logger.Error($"Comunication api retun fail - {response.RequestMessage} {response.ReasonPhrase}");
            return jsonList;
        }

        var result = response.Content.ReadAsStreamAsync().Result;
        var json = JsonDocument.ParseAsync(result).Result;

        nextRequest = json.RootElement.GetProperty("next").GetString();

        JsonElement prop = json.RootElement.GetProperty("results");

        foreach (var item in prop.EnumerateArray())
            jsonList.Add(item);

        return jsonList;
    }

    private List<Movie> ConvertJsonToPlanet(List<JsonElement> elements)
    {
        var movies = new List<Movie>();
        short cont = 1;
        foreach (var element in elements)
        {
            var title = element.GetProperty("title").GetString()!;
            var episode = element.GetProperty("episode_id").GetInt16()!;
            var openingCrawl = element.GetProperty("opening_crawl").GetString()!;
            var director = element.GetProperty("director").GetString()!;
            var producer = element.GetProperty("producer").GetString()!;
            var releaseDate = element.GetProperty("release_date").GetString()!;

            CharactersIds.Add(cont, GetIdFromJsonElements(element.GetProperty("characters")));
            //Characters.AddRange(GetIdFromJsonElements(element.GetProperty("characters")));
            //var characters = Characters.Where(x => charactersId.Contains(x.Id)).ToList();

            PlanetsIds.Add(cont, GetIdFromJsonElements(element.GetProperty("planets")));
            //var planets = Planets.Where(x => planetsIds.Contains(x.Id)).ToList();

            StarshipsIds.Add(cont, GetIdFromJsonElements(element.GetProperty("starships")));
            //var starShips = Starships.Where(x => starShipsIds.Contains(x.Id)).ToList();

            VehiclesIds.Add(cont, GetIdFromJsonElements(element.GetProperty("vehicles")));
            //var vehicles = Vehicles.Where(x => starShipsIds.Contains(x.Id)).ToList();

            movies.Add(new Movie(cont, title, episode, openingCrawl, director, producer, releaseDate));

            cont++;
        }

        return movies;
    }
}
