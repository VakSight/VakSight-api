namespace VakSight.Repository.Implementation.Database
{
    public class DatabaseConfig
    {
        public string Server { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int CommandTimeout { get; set; }

        public bool IsRemote { get; set; }
    }
}
