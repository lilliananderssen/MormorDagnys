namespace MormorDagnys.Entities;

// Kopplar en leverantör till en produkt för att lagra vilket pris just den leverantören tar för just den produkten
public class SupplierProduct
{
    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; } = null!;

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public decimal PricePerKg { get; set; }
}
