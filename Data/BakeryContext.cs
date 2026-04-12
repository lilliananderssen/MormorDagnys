using Microsoft.EntityFrameworkCore;
using MormorDagnys.Entities;

namespace MormorDagnys.Data;

// Hanterar all databasåtkomst för att ge applikationen ett gemensamt ställe att läsa och skriva data
public class BakeryContext(DbContextOptions<BakeryContext> options) : DbContext(options)
{
    public DbSet<Supplier> Suppliers { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<SupplierProduct> SupplierProducts { get; set; } = null!;

    // Konfigurerar en sammansatt primärnyckel på SupplierProduct för att förhindra att samma leverantör kopplas till samma produkt två gånger
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SupplierProduct>()
            .HasKey(x => new { x.SupplierId, x.ProductId });
    }
}
