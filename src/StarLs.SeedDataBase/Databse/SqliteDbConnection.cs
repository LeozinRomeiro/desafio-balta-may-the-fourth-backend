using System.Data.SQLite;

namespace StarLs.SeedDataBase.Databse;
internal class SqliteDbConnection : IDbConnection<SQLiteConnection>, IDisposable
{
    public readonly string _connStr = @"DataSource=C:\csharp\Balta\desafio-balta-may-the-fourth-backend\src\StarLs.Api\app.db;version=3;";
    private SQLiteConnection _conn;

    public void Dispose()
    {
        _conn.Close();
    }

    public SQLiteConnection GetConnection()
    {
        _conn = new SQLiteConnection(_connStr);
        _conn.Open();

        return _conn;
    }

}
