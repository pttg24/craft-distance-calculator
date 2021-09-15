namespace craft.Configuration
{
    public interface IDatabaseConfiguration
    {
        public string CollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}
