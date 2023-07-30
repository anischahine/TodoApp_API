using TodoApp_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Security.Principal;

namespace TodoApp_API.Services
{
    public class MongoDBService
    {
        private IMongoDatabase database = null!;
        private IOptions<MongoDBSettings> _mongoDBSettings;

        public AccountService _accountService = null!;
        public TaskService _taskService = null!;

        public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            _mongoDBSettings = mongoDBSettings;
            var mongoClient = new MongoClient(
                mongoDBSettings.Value.ConnectionString);
            database = mongoClient.GetDatabase(
                mongoDBSettings.Value.DatabaseName);
        }
        public void GetAccountCollection()
        {
            _accountService = new AccountService(database.GetCollection<Account>(
                _mongoDBSettings.Value.AccountCollectionName));
        }
        public void GetTaskCollection()
        {
            _taskService = new TaskService(database.GetCollection<Tasks>(
                _mongoDBSettings.Value.TaskCollectionName));
        }
    }
}
