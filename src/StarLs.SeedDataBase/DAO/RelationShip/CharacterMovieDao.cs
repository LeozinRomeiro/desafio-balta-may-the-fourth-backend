using StarLs.SeedDataBase.Databse;
using System.Data.SQLite;
using System.Text;
using Serilog;
using Dapper;

namespace StarLs.SeedDataBase.DAO.RelationShip;
internal class CharacterMovieDao(ILogger logger)
{
    private SQLiteConnection _conn = null!;
    private readonly ILogger _logger = logger;

    public async void Create(Dictionary<short, List<short>> characterMovieIds, Dictionary<short, List<short>> movieCharacterIds)
    {
        if (characterMovieIds.Count <= 0 || movieCharacterIds.Count <= 0)
        {
            _logger.Warning("list for relation is empty");
            return;
        }

        try
        {
            var queryBuiler = new StringBuilder();
            queryBuiler.Append(@"INSERT INTO CharacterMovie (CharactersId,  MoviesId) VALUES ");
            queryBuiler.Append(@"(@CharactersId, @MoviesId)");

            using (_conn = new SqliteDbConnection().GetConnection())
            {
                using var transaction = await _conn.BeginTransactionAsync();
                foreach (var idCharacter in characterMovieIds)
                {
                    foreach (var idMovie in idCharacter.Value)
                    {
                        await _conn.ExecuteAsync(
                                    queryBuiler.ToString(),
                                    new
                                    {
                                        CharactersId = idCharacter.Key,
                                        MoviesId = idMovie
                                    },
                                    transaction
                               );
                    }
                }

                await transaction.CommitAsync();
            }

            _logger.Information("Relationship between character and Movie was completed successfully!");
        }
        catch (SQLiteException ex)
        {
            _logger.Error($"Database error - {ex.Message}");
        }
        catch (Exception ex)
        {
            _logger.Error($"Error Character Movie Dao {ex.Message}");
        }

    }
}
