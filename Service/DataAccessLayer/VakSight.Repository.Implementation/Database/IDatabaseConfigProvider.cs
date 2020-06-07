namespace VakSight.Repository.Implementation.Database
{
    public interface IDatabaseConfigProvider
    {
        DatabaseConfig Get(string database);

        string GetConnectionString(string database);
    }
}
