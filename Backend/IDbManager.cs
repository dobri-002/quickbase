namespace Backend;

public interface IDbManager
{
    DbConnection GetConnection();
}
