using Serilog;
using System.Net.WebSockets;
using System.Text.Json;

namespace StarLs.SeedDataBase.Request;
internal class BaseRequest(HttpClient httpClient, ILogger logger)
{
    protected readonly HttpClient _httpClient = httpClient;
    protected readonly ILogger _logger = logger;
    protected string _url = "https://swapi.py4e.com/api/";
    protected string _endPoint = null!;

    protected List<short> GetIdFromJsonElements(JsonElement jsonElements)
    {
        var ids = new List<short>();

        foreach (var element in jsonElements.EnumerateArray())
        {
            try
            {
                ids.Add(short.Parse(element.GetString()!.Replace(_url, "").Split("/")[1]));
            }
            catch (FormatException ex)
            {
                _logger.Warning($"Invalid short parse fail with. {element} - {ex.Message}");
            }
            catch
            {
                _logger.Warning($"unhandled exception short parse. {element}");
            }
        }

        return ids;
    }
    protected short GetIdFromJsonElements(string jsonStr)
    {
        short id = 0;

        try
        {
            id = (short.Parse(jsonStr.Replace(_url, "").Split("/")[1]));
        }
        catch (FormatException ex)
        {
            _logger.Warning($"Invalid short parse fail with. {jsonStr} - {ex.Message}");
        }
        catch(Exception ex)
        {
            _logger.Warning($"unhandled exception short parse. {jsonStr} - {ex.Message}");
        }

        return id;
    }
}
