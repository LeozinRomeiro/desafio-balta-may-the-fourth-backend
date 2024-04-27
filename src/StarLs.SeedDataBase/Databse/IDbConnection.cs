namespace StarLs.SeedDataBase.Databse;
internal interface IDbConnection<T>
{
    public T GetConnection();
}
