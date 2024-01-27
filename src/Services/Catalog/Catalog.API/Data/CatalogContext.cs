using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("DatabaseSettings:ConnectionString");
            var client = new MongoClient(connectionString);
            var dbName = configuration.GetValue<string>("DatabaseSettings:DatabaseName");
            var database = client.GetDatabase(dbName);
            var tableName = configuration.GetValue<string>("DatabaseSettings:CollectionName");
            Products = database.GetCollection<Product>(tableName);
            CatalogContextSeed.SeedData(Products);
        }
        public IMongoCollection<Product> Products { get; }
    }
}
