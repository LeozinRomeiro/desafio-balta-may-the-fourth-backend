using Serilog;
using StarLs.Core.Entities;
using System.Reflection;
using System.Text.Json;

namespace StarLs.SeedDataBase.Request;
internal class CharacterRequest : BaseRequest
{
    public Dictionary<short, List<short>> MoviesIds { get; set; } = [];
    public Dictionary<short, List<short>> VehiclesIds { get; set; } = [];

    public CharacterRequest(IHttpClientFactory httpClient, ILogger logger) : base(httpClient.CreateClient("https://swapi.py4e.com/api/"), logger)
    {
        _endPoint = "people";
        _httpClient.BaseAddress = new Uri(_url);
    }

    public List<Character> Get()
    {
        var character = new List<Character>();
        try
        {
            List<JsonElement> jsonList = [];
            string? nextRequest;
            do
            {
                jsonList.AddRange(MakeRequest(out nextRequest));
                _endPoint = nextRequest ?? _endPoint;
            } while (nextRequest != null);

            character = ConvertJsonToPlanet(jsonList);
        }
        catch (Exception ex)
        {
            _logger.Error($"Comunication api fail. unhandled exception - {ex.Message}");
        }

        return character;
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

    private List<Character> ConvertJsonToPlanet(List<JsonElement> elements)
    {
        var planets = new List<Character>();
        short cont = 1;
        foreach (var element in elements)
        {
            var name = element.GetProperty("name").GetString()!;
            var height = element.GetProperty("height").GetString()!;
            var weight = element.GetProperty("mass").GetString()!;
            var hairColor = element.GetProperty("hair_color").GetString()!;
            var skinColor = element.GetProperty("skin_color").GetString()!;
            var eyeColor = element.GetProperty("eye_color").GetString()!;
            var birthYear = element.GetProperty("birth_year").GetString()!;
            var gender = element.GetProperty("gender").GetString()!;
            var planetId = GetIdFromJsonElements(element.GetProperty("homeworld").GetString()!);

            MoviesIds.Add(cont, GetIdFromJsonElements(element.GetProperty("films")));
            //var movies = Movies.Where(x => moviesIds.Contains(x.Id)).ToList();

            VehiclesIds.Add(cont, GetIdFromJsonElements(element.GetProperty("vehicles")));
            //var vehicles = Movies.Where(x => vehiclesIds.Contains(x.Id)).ToList();

            planets.Add(new Character(cont, name, height, weight, hairColor, skinColor, eyeColor, birthYear, gender, planetId));

            cont++;
        }

        return planets;
    }
}
