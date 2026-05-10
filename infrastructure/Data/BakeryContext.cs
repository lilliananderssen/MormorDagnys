using core.Entities;
using core.Entities.Orders;
using infrastructure.Config;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Data;

public class BakeryContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<SupplierProduct> SupplierProducts { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<BakeryProduct> BakeryProducts { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(CustomerConfiguration).Assembly);
    }
}
