namespace craft.Configuration
{
    public class DatabaseConfiguration : IDatabaseConfiguration
    {
        public string CollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}
