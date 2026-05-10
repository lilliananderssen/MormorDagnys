using System.Reflection;
using System.Text.Json;
using core.Entities;

namespace infrastructure.Data;

public class SeedDatabase
{
    private static readonly JsonSerializerOptions options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public static async Task SeedSuppliers(BakeryContext context)
    {
        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        if (context.Suppliers.Any()) return;

        var json = File.ReadAllText(path + @"/Data/Json/suppliers.json");
        var suppliers = JsonSerializer.Deserialize<List<Supplier>>(json, options);

        if (suppliers is not null)
        {
            await context.Suppliers.AddRangeAsync(suppliers);
            await context.SaveChangesAsync();
        }
    }

    public static async Task SeedProducts(BakeryContext context)
    {
        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        if (context.Products.Any()) return;

        var json = File.ReadAllText(path + @"/Data/Json/products.json");
        var products = JsonSerializer.Deserialize<List<Product>>(json, options);

        if (products is not null && products.Count > 0)
        {
            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }
    }

    public static async Task SeedCustomers(BakeryContext context)
    {
        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        if (context.Customers.Any()) return;

        var json = File.ReadAllText(path + @"/Data/Json/customers.json");
        var customers = JsonSerializer.Deserialize<List<Customer>>(json, options);

        if (customers is not null)
        {
            await context.Customers.AddRangeAsync(customers);
            await context.SaveChangesAsync();
        }
    }

    public static async Task SeedBakeryProducts(BakeryContext context)
    {
        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        if (context.BakeryProducts.Any()) return;

        var json = File.ReadAllText(path + @"/Data/Json/bakeryproducts.json");
        var bakeryProducts = JsonSerializer.Deserialize<List<BakeryProduct>>(json, options);

        if (bakeryProducts is not null && bakeryProducts.Count > 0)
        {
            var now = DateTime.Now;
            foreach (var p in bakeryProducts)
            {
                p.ManufacturingDate = now;
                p.BestBefore = now.AddDays(5);
            }
            await context.BakeryProducts.AddRangeAsync(bakeryProducts);
            await context.SaveChangesAsync();
        }
    }
}
