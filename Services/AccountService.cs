using TodoApp_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Security.Principal;

namespace TodoApp_API.Services
{
    public class AccountService
    {
        private readonly IMongoCollection<Account> _accountCollection;

        public AccountService(IMongoCollection<Account> accountCollection) 
        {
            _accountCollection = accountCollection;
        }

        public async Task<List<Account>> GetAsync() =>
            await _accountCollection.Find(_ => true).ToListAsync();

        public async Task<Account?> GetAsync(string id) =>
            await _accountCollection.Find(x => x._Id == id).FirstOrDefaultAsync();

        public async Task<Account?> GetAsync(string email, string password) =>
            await _accountCollection.Find(x => x.email == email && x.password == password && x.isActive == true).FirstOrDefaultAsync();

        public async Task CreateAsync(Account newAccount) =>
            await _accountCollection.InsertOneAsync(newAccount);

        public async Task UpdateAsync(string id, Account updatedAccount) =>
            await _accountCollection.ReplaceOneAsync(x => x._Id == id, updatedAccount);

        public async Task RemoveAsync(string id) =>
            await _accountCollection.DeleteOneAsync(x => x._Id == id);
    }
}
