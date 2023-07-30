using TodoApp_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Security.Principal;
using MongoDB.Bson;

namespace TodoApp_API.Services
{
    public class TaskService
    {
        private readonly IMongoCollection<Tasks> _taskCollection;

        public TaskService(IMongoCollection<Tasks> taskCollection)
        {
            _taskCollection = taskCollection;
        }

        public async Task<List<Tasks>> GetAsync() =>
            await _taskCollection.Find(_ => true).ToListAsync();

        public async Task<Tasks?> GetAsync(string id) =>
            await _taskCollection.Find(x => x._Id == id).FirstOrDefaultAsync();

        public async Task<List<Tasks>?> GetAsync(string email, string state) =>
            await _taskCollection.Find(x => x.accountEmail == email && x.state == state ).ToListAsync();

        public async Task CreateAsync(Tasks newTask) =>
            await _taskCollection.InsertOneAsync(newTask);

        public async Task UpdateAsync(string id, Tasks updatedTask) =>
            await _taskCollection.ReplaceOneAsync(x => x._Id == id, updatedTask);

        public async Task RemoveAsync(string id) =>
            await _taskCollection.DeleteOneAsync(x => x._Id == id);
    }
}