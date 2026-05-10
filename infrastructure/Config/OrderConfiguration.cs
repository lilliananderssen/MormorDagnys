using core.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace infrastructure.Config;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasMany(c => c.OrderItems).WithOne(i => i.Order).HasForeignKey(i => i.OrderId).OnDelete(DeleteBehavior.Cascade);
        builder.Property(c => c.OrderDate).HasConversion(
            c => c.ToUniversalTime(),
            c => DateTime.SpecifyKind(c, DateTimeKind.Utc)
        );
    }
}
