using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment7.Models
{
    public class UserDb
    {
        private readonly IMongoCollection<UserData> collection;

        public UserDb()
        {
            var client = new MongoClient("mongodb://127.0.0.1:27017/");
            var database = client.GetDatabase("user");
            collection = database.GetCollection<UserData>("user_details");
        }

        public async Task<List<UserData>> GetAllAsync()
        {
            return await collection.Find(_ => true).ToListAsync();
        }

        public async Task<UserData> GetByIdAsync(string id)
        {
            return await collection.Find(_ => _.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddNewUserAsync(UserData newUser)
        {
            await collection.InsertOneAsync(newUser);
        }

        public async Task UpdateUserAsync(UserData userData)
        {
            await collection.ReplaceOneAsync(x => x.Id == userData.Id, userData);
        }
        public async Task DeleteUserAsync(string id)
        {
            await collection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
