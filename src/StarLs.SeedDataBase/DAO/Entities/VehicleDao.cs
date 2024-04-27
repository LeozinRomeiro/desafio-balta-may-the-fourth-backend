using Dapper;
using StarLs.Core.Entities;
using StarLs.SeedDataBase.Databse;
using Serilog;
using System.Data.SQLite;
using System.Text;

namespace StarLs.SeedDataBase.DAO.Entities;
internal class VehicleDao(ILogger logger)
{
    private SQLiteConnection _conn = null!;
    private readonly ILogger _logger = logger;

    public async void Create(List<Vehicle> characters)
    {
        if (characters.Count <= 0)
        {
            _logger.Warning("Vehicle list is empty");
            return;
        }

        try
        {
            var queryBuiler = new StringBuilder();
            queryBuiler.Append(@"INSERT INTO Vehicle (Name, Model, Manufacturer, CostInCredits, Length, 
                                MaxSpeed, Crew, Passengers, CargoCapacity, Consumables, Class) VALUES ");
            queryBuiler.Append(@"(@Name, @Model, @Manufacturer, @CostInCredits, @Length, 
                                @MaxSpeed, @Crew, @Passengers, @CargoCapacity, @Consumables, @Class)");

            using (_conn = new SqliteDbConnection().GetConnection())
            {
                using var transaction = await _conn.BeginTransactionAsync();
                for (int i = 0; i < characters.Count; i++)
                    await _conn.ExecuteAsync(queryBuiler.ToString(), characters[i], transaction);

                await transaction.CommitAsync();
            }

            _logger.Information("Vehicle table completed successfully!");
        }
        catch (SQLiteException ex)
        {
            _logger.Error($"Database error - {ex.Message}");
        }
        catch (Exception ex)
        {
            _logger.Error($"Error vehicle dao {ex.Message}");
        }

    }
}
