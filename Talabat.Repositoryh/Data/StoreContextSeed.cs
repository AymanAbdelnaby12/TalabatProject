using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Models;
using Talabat.Core.Models.Order_Aggregate;

namespace Talabat.Repository.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext DbContext)
        {
            
            // seed data for product Brands
            if (!DbContext.Set<ProductBrand>().Any())
            {
                var baseDirectory = AppContext.BaseDirectory;
                var BrandsData = File.ReadAllText(Path.Combine(baseDirectory, "DataSeeding", "brands.json"));
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
                if (brands?.Count > 0)
                {
                    foreach (var brand in brands)
                    {
                        await DbContext.Set<ProductBrand>().AddAsync(brand);
                    }
                    await DbContext.SaveChangesAsync();
                }
           
            }

            // seed data for product Types
            if (!DbContext.Set<ProductType>().Any())
            {
                var TypesData = File.ReadAllText("C:\\Users\\AymanAbdelnaby\\source\\repos\\Talabat\\Talabat.Repositoryh\\Data\\DataSeeding\\types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);
                if (types?.Count > 0)
                {
                    foreach (var type in types)
                    {
                        await DbContext.Set<ProductType>().AddAsync(type);
                    }
                    await DbContext.SaveChangesAsync();
                }  
            }

            // seed data for products
            if (!DbContext.Set<Product>().Any())
            {
                var baseDirectory = AppContext.BaseDirectory;
                var ProductsData = File.ReadAllText("C:\\Users\\AymanAbdelnaby\\source\\repos\\Talabat\\Talabat.Repositoryh\\Data\\DataSeeding\\products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                if (products?.Count > 0)
                {
                    foreach (var product in products)
                    {
                        await DbContext.Set<Product>().AddAsync(product);
                    }
                    await DbContext.SaveChangesAsync();
                }
            }

            // seed data for delivery methods
            if (!DbContext.Set<DeliveryMethod>().Any())
            {

                var DeliveryMethodData = File.ReadAllText("C:\\Users\\AymanAbdelnaby\\source\\repos\\Talabat\\Talabat.Repositoryh\\Data\\DataSeeding\\delivery.json");
                var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodData);
                if (DeliveryMethods?.Count > 0)  
                {
                    foreach (var DeliveryMethod in DeliveryMethods)
                    {
                        await DbContext.Set<DeliveryMethod>().AddAsync(DeliveryMethod);
                    }
                    await DbContext.SaveChangesAsync();
                }
            }
        }
    }
}
