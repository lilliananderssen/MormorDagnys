using core.Entities;
using core.Interfaces;
using infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Services;

public class SupplierProductService(BakeryContext context) : ISupplierProductService
{
    public async Task<bool> AnyAsync(int supplierId, int productId)
    {
        return await context.SupplierProducts
            .AnyAsync(sp => sp.SupplierId == supplierId && sp.ProductId == productId);
    }

    public async Task AddAsync(int supplierId, int productId, decimal pricePerKg)
    {
        var entry = new SupplierProduct
        {
            SupplierId = supplierId,
            ProductId = productId,
            PricePerKg = pricePerKg
        };
        context.SupplierProducts.Add(entry);
        await context.SaveChangesAsync();
    }

    public async Task<SupplierProduct?> FindAsync(int supplierId, int productId)
    {
        return await context.SupplierProducts
            .FirstOrDefaultAsync(sp => sp.SupplierId == supplierId && sp.ProductId == productId);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}
