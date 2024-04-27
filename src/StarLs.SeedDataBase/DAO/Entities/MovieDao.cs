using Dapper;
using Serilog;
using StarLs.Core.Entities;
using StarLs.SeedDataBase.Databse;
using System.Data.SQLite;
using System.Text;

namespace StarLs.SeedDataBase.DAO.Entities;
internal class MovieDao(ILogger logger)
{
    private SQLiteConnection _conn = null!;
    private readonly ILogger _logger = logger;

    public async void Create(List<Movie> movies)
    {
        if (movies.Count <= 0)
        {
            _logger.Warning("Movies list is empty");
            return;
        }

        try
        {
            var queryBuiler = new StringBuilder();
            queryBuiler.Append("INSERT INTO Movie (Title, Episode, OpeningCrawl, Director, Producer, ReleaseDate) VALUES ");
            queryBuiler.Append("(@Title, @Episode, @OpeningCrawl, @Director, @Producer, @ReleaseDate)");

            using (_conn = new SqliteDbConnection().GetConnection())
            {
                using var transaction = await _conn.BeginTransactionAsync();
                for (int i = 0; i < movies.Count; i++)
                    await _conn.ExecuteAsync(queryBuiler.ToString(), movies[i], transaction);

                await transaction.CommitAsync();
            }

            _logger.Information("Movies table completed successfully!");
        }
        catch (SQLiteException ex)
        {
            _logger.Error($"Database error - {ex.Message}");
        }
        catch (Exception ex)
        {
            _logger.Error($"Error movie dao {ex.Message}");
        }

    }
}
