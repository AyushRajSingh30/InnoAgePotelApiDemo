using InnoAgePotelApi.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace InnoAgePotelApi.DbContext
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration config)
        {
            var connectionString = config.GetSection("MongoDbConnection")["ConnectionString"];
            var databaseName = config.GetSection("MongoDbConnection")["Database"];

            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        // Generic Collections
        public IMongoCollection<Post> Posts => _database.GetCollection<Post>("Posts");
        public IMongoCollection<Like> Like => _database.GetCollection<Like>("Like");
        public IMongoCollection<Comment> Comment => _database.GetCollection<Comment>("Comment");
        public IMongoCollection<Poll> Poll => _database.GetCollection<Poll>("Poll");
    }
}
