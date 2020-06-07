namespace VakSight.Repository.Implementation.Database
{
    public interface IDatabaseConfigReader
    {
        DatabaseConfig Get(string database);
    }
}
