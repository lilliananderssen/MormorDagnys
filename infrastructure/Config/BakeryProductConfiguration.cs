using core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace infrastructure.Config;

public class BakeryProductConfiguration : IEntityTypeConfiguration<BakeryProduct>
{
    public void Configure(EntityTypeBuilder<BakeryProduct> builder)
    {
        builder.Property(c => c.PricePerUnit).HasColumnType("decimal(18,2)");
        builder.Property(c => c.WeightGrams).HasColumnType("decimal(18,2)");
        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
    }
}
