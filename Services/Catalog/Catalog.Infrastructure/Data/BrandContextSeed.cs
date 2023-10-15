using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data;

public static class BrandContextSeed
{ 
    public static void SeedData(IMongoCollection<ProductBrand> brandCollection)
    {
        bool checkBrands = brandCollection.Find(brand => true).Any();


        if (!checkBrands)
        {
            var brandsData = File.ReadAllText("../Catalog.Infrastructure/Data/SeedData/brands.json");
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
            if (brands != null)
            {
                foreach(ProductBrand brand in brands)
                {
                    brandCollection.InsertOneAsync(brand);
                }
            }
        }
    }
}
