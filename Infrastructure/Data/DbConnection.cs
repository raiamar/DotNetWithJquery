public class DbConnection : IDbConnection
{
    private readonly string _connectionString;

    public DbConnection(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }
    public string GetConnectionString()
    {
        return _connectionString;
    }
}
