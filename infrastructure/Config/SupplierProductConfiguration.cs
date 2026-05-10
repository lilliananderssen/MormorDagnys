using core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace infrastructure.Config;

public class SupplierProductConfiguration : IEntityTypeConfiguration<SupplierProduct>
{
    public void Configure(EntityTypeBuilder<SupplierProduct> builder)
    {
        builder.HasKey(x => new { x.SupplierId, x.ProductId });
        builder.Property(c => c.PricePerKg).HasColumnType("decimal(18,2)");
    }
}
