namespace TodoApp_API.Models
{
    public class MongoDBSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string AccountCollectionName { get; set; } = null!;
        public string TaskCollectionName { get; set; } = null!;
    }
}
