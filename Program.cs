using Microsoft.EntityFrameworkCore;
using MormorDagnys.Data;
using MormorDagnys.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Registrerar BakeryContext med SQLite för att ge applikationen tillgång till databasen
builder.Services.AddDbContext<BakeryContext>(options =>
    options.UseSqlite("Data Source=bakery.db"));

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

// Skapar databasen och fyller den med testdata för att applikationen ska ha något att arbeta med direkt vid uppstart
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BakeryContext>();
    context.Database.EnsureCreated();

    if (!context.Suppliers.Any())
    {
        var s1 = new Supplier { Name = "Lantmännen Cerealia AB", Address = "Karlavägen 164, 104 35 Stockholm", ContactPerson = "Maria Lindqvist", Phone = "0105560000", Email = "maria.lindqvist@lantmannen.com" };
        var s2 = new Supplier { Name = "Nordic Sugar AB", Address = "Munkvägen 2, 232 81 Arlöv", ContactPerson = "Johan Petersson", Phone = "0405350000", Email = "johan.petersson@nordicsugar.com" };
        var s3 = new Supplier { Name = "Fazer Sverige AB", Address = "Herrhagsvägen 2, 120 26 Stockholm", ContactPerson = "Sara Bergström", Phone = "0862210000", Email = "sara.bergstrom@fazer.com" };

        var p1 = new Product { ArticleNumber = "R001", Name = "Vetemjöl" };
        var p2 = new Product { ArticleNumber = "R002", Name = "Socker" };
        var p3 = new Product { ArticleNumber = "R003", Name = "Smör" };

        context.Suppliers.AddRange(s1, s2, s3);
        context.Products.AddRange(p1, p2, p3);
        context.SaveChanges();

        context.SupplierProducts.AddRange(
            new SupplierProduct { SupplierId = s1.SupplierId, ProductId = p1.ProductId, PricePerKg = 12.50m },
            new SupplierProduct { SupplierId = s3.SupplierId, ProductId = p1.ProductId, PricePerKg = 13.20m },
            new SupplierProduct { SupplierId = s2.SupplierId, ProductId = p2.ProductId, PricePerKg = 8.75m },
            new SupplierProduct { SupplierId = s3.SupplierId, ProductId = p2.ProductId, PricePerKg = 9.10m },
            new SupplierProduct { SupplierId = s3.SupplierId, ProductId = p3.ProductId, PricePerKg = 42.00m }
        );
        context.SaveChanges();
    }
}

app.Run();
