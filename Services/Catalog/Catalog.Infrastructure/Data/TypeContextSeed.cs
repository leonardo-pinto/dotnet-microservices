using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data;

public static class TypeContextSeed
{ 
    public static void SeedData(IMongoCollection<ProductType> typeCollection)
    {
        bool checkTypes = typeCollection.Find(brand => true).Any();


        if (!checkTypes)
        {
            var typesData = File.ReadAllText("../Catalog.Infrastructure/Data/SeedData/types.json");
            var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
            if (types != null)
            {
                foreach(ProductType type in types)
                {
                    typeCollection.InsertOneAsync(type);
                }
            }
        }
    }
}
