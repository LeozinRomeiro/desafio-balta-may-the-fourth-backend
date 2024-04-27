using System.Data.SQLite;
using System.IO;

namespace StarLs.SeedDataBase.Databse;
internal class SqliteDbConnection : IDbConnection<SQLiteConnection>, IDisposable
{
    public readonly string _connStr = "";
    private SQLiteConnection _conn;

    public void Dispose()
    {
        _conn.Close();
    }

    public SQLiteConnection GetConnection()
    {
        var currentDic = Directory.GetCurrentDirectory();
        var pathDatabase = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(currentDic).ToString()).ToString()).ToString());
        var _connStr = $"DataSource={Path.Combine(pathDatabase.FullName, "StarLs.Api", "app.db")},version=3;";

        _conn = new SQLiteConnection(_connStr);
        _conn.Open();

        return _conn;
    }

}
