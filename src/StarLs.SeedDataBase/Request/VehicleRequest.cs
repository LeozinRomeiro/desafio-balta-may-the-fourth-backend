using Serilog;
using StarLs.Core.Entities;
using System.Text.Json;

namespace StarLs.SeedDataBase.Request;
internal class VehicleRequest : BaseRequest
{
    public Dictionary<short, List<short>> MoviesIds { get; set; } = [];

    public VehicleRequest(IHttpClientFactory httpClient, ILogger logger) : base(httpClient.CreateClient("https://swapi.py4e.com/api/"), logger)
    {
        _endPoint = "vehicles";
        _httpClient.BaseAddress = new Uri(_url);
    }

    public List<Vehicle> Get()
    {
        var vehicles = new List<Vehicle>();
        try
        {
            List<JsonElement> jsonList = [];
            string? nextRequest;
            do
            {
                jsonList.AddRange(MakeRequest(out nextRequest));
                _endPoint = nextRequest ?? _endPoint;
            } while (nextRequest != null);

            vehicles = ConvertJsonToPlanet(jsonList);
        }
        catch (Exception ex)
        {
            _logger.Error($"Comunication api fail. unhandled exception - {ex.Message}");
        }

        return vehicles;
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

    private List<Vehicle> ConvertJsonToPlanet(List<JsonElement> elements)
    {
        var vehicles = new List<Vehicle>();
        short cont = 1;
        foreach (var element in elements)
        {
            var model = element.GetProperty("model").GetString()!;
            var name = element.GetProperty("name").GetString()!;
            var vehicleClass = element.GetProperty("vehicle_class").GetString()!;
            var manufacturer = element.GetProperty("manufacturer").GetString()!;
            var costInCredits = element.GetProperty("cost_in_credits").GetString()!;
            var lenght = element.GetProperty("length").GetString()!;
            var maxSpeed = element.GetProperty("max_atmosphering_speed").GetString()!;
            var crew = element.GetProperty("crew").GetString()!;
            var passengers = element.GetProperty("passengers").GetString()!;
            var maxAtmospheringSpeed = element.GetProperty("max_atmosphering_speed").GetString()!;
            var cargoCapacity = element.GetProperty("cargo_capacity").GetString()!;
            var consumables = element.GetProperty("consumables").GetString()!;
            var clas = element.GetProperty("vehicle_class").GetString()!;

            MoviesIds.Add(cont, GetIdFromJsonElements(element.GetProperty("films")));
            //var movies = Movies.Where(x => moviesId.Contains(x.Id)).ToList();

            vehicles.Add(new Vehicle(cont, name, model, manufacturer, costInCredits, lenght, maxSpeed, crew, passengers, cargoCapacity,
                consumables, clas));

            cont++;
        }

        return vehicles;
    }
}
