using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data;

public static class CatalogContextSeed
{ 
    public static void SeedData(IMongoCollection<Product> productCollection)
    {
        bool checkProducts = productCollection.Find(brand => true).Any();

        if (!checkProducts)
        {
            var productsData = File.ReadAllText("../Catalog.Infrastructure/Data/SeedData/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(productsData);
            if (products != null)
            {
                foreach(Product product in products)
                {
                    productCollection.InsertOneAsync(product);
                }
            }
        }
    }
}
