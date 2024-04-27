using StarLs.Core.Entities;
using StarLs.SeedDataBase.Databse;
using Serilog;
using System.Data.SQLite;
using System.Text;
using Dapper;

namespace StarLs.SeedDataBase.DAO.Entities;
internal class CharacterDao
{
    private SQLiteConnection _conn = null!;
    private readonly ILogger _logger;

    public CharacterDao(ILogger logger) => _logger = logger;

    public async void Create(List<Character> characters)
    {
        if (characters.Count <= 0)
        {
            _logger.Warning("Character list is empty");
            return;
        }

        try
        {
            var queryBuiler = new StringBuilder();
            queryBuiler.Append("INSERT INTO Character (Name, Height, Weight, HairColor, SkinColor, EyeColor, BirthYear, Gender, PlanetId) VALUES ");
            queryBuiler.Append("(@Name, @Height, @Weight, @HairColor, @SkinColor, @EyeColor, @BirthYear, @Gender, @PlanetId)");

            using (_conn = new SqliteDbConnection().GetConnection())
            {
                using (var transaction = await _conn.BeginTransactionAsync())
                {
                    for (int i = 0; i < characters.Count; i++)
                        await _conn.ExecuteAsync(queryBuiler.ToString(), characters[i], transaction);

                    await transaction.CommitAsync();
                }
            }

            _logger.Information("Characters table completed successfully!");
        }
        catch (SQLiteException ex)
        {
            _logger.Error($"Database error - {ex.Message}");
        }
        catch (Exception ex)
        {
            _logger.Error($"Error character dao {ex.Message}");
        }

    }
}
