using Serilog;
using StarLs.Core.Entities;
using System.Text.Json;

namespace StarLs.SeedDataBase.Request;
internal class PlanetRequest : BaseRequest
{
    public Dictionary<short, List<short>> Characters { get; set; } = [];
    public Dictionary<short, List<short>> Movies { get; set; } = [];

    public PlanetRequest(IHttpClientFactory httpClient, ILogger logger) : base(httpClient.CreateClient("https://swapi.py4e.com/api/"), logger)
    {
        _endPoint = "planets";
        _httpClient.BaseAddress = new Uri(_url);
    }
       
    public List<Planet> Get()
    {
        var planets = new List<Planet>();
        try
        {
            List<JsonElement> jsonList = [];
            string? nextRequest;
            do
            {
                jsonList.AddRange(MakeRequest(out nextRequest));
                _endPoint = nextRequest ?? _endPoint;
            } while (nextRequest != null);

            planets = ConvertJsonToPlanet(jsonList);
        }
        catch (Exception ex)
        {
            _logger.Error($"Comunication api fail. unhandled exception - {ex.Message}");
        }

        return planets;
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

    private List<Planet> ConvertJsonToPlanet(List<JsonElement> elements)
    {
        var planets = new List<Planet>();
        short cont = 1;
        foreach (var element in elements)
        {
            var name = element.GetProperty("name").GetString()!;
            var diameter = element.GetProperty("diameter").GetString()!;
            var rotationSpeed = element.GetProperty("rotation_period").GetString()!;
            var orbitalPeriod = element.GetProperty("orbital_period").GetString()!;
            var gravity = element.GetProperty("gravity").GetString()!;
            var population = element.GetProperty("population").GetString()!;
            var climate = element.GetProperty("climate").GetString()!;
            var terrain = element.GetProperty("terrain").GetString()!;
            var surfaceWater = element.GetProperty("surface_water").GetString()!;

            Characters.Add(cont, GetIdFromJsonElements(element.GetProperty("residents")));
            //var characters = Characters.Where(x => charactersId.Contains(x.Id)).ToList();

            Movies.Add(cont, GetIdFromJsonElements(element.GetProperty("films")));
            //var movies = Movies.Where(x => moviesId.Contains(x.Id)).ToList();

            planets.Add(new Planet(cont, name, rotationSpeed, orbitalPeriod, diameter, 
                climate, gravity, terrain, surfaceWater, population));

            cont++;
        }

        return planets;
    }
}

