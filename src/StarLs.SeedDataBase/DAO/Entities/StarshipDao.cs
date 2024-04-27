using StarLs.Core.Entities;
using StarLs.SeedDataBase.Databse;
using System.Data.SQLite;
using System.Text;
using Serilog;
using Dapper;
using System.Reflection;
using System.Security.Claims;
using System.Xml.Linq;

namespace StarLs.SeedDataBase.DAO.Entities;
internal class StarshipDao(ILogger logger)
{
    private SQLiteConnection _conn = null!;
    private readonly ILogger _logger = logger;

    public async void Create(List<Starship> starShips)
    {
        if (starShips.Count <= 0)
        {
            _logger.Warning("Starships list is empty");
            return;
        }

        try
        {
            var queryBuiler = new StringBuilder();
            queryBuiler.Append(@"INSERT INTO Starship (Name, Model, Manufacturer, CostInCredits, Length, 
                                MaxSpeed, Crew, Passengers, CargoCapacity, HyperdriveRating, Mglt, Consumables, Class) VALUES");
            queryBuiler.Append(@"(@Name, @Model, @Manufacturer, @CostInCredits, @Length,
                                @MaxSpeed, @Crew, @Passengers, @CargoCapacity, @HyperdriveRating, @Mglt, @Consumables, @Class)");

            using (_conn = new SqliteDbConnection().GetConnection())
            {
                using var transaction = await _conn.BeginTransactionAsync();
                for (int i = 0; i < starShips.Count; i++)
                    await _conn.ExecuteAsync(queryBuiler.ToString(), starShips[i], transaction);
                await transaction.CommitAsync();
            }
            _logger.Information("Starship table completed successfully!");
        }
        catch (SQLiteException ex)
        {
            _logger.Error($"Database error - {ex.Message}");
        }
        catch (Exception ex)
        {
            _logger.Error($"Error starship dao {ex.Message}");
        }

    }
}
