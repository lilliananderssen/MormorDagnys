using core.Entities;

namespace core.Interfaces;

public interface ISupplierProductService
{
    Task<bool> AnyAsync(int supplierId, int productId);
    Task AddAsync(int supplierId, int productId, decimal pricePerKg);
    Task<SupplierProduct?> FindAsync(int supplierId, int productId);
    Task SaveChangesAsync();
}
