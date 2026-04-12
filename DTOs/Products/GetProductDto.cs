namespace MormorDagnys.DTOs.Products;

// Returnerar produktdata till klienten för att inte skicka med mer information än vad som behövs
public class GetProductDto
{
    public int ProductId { get; set; }
    public string ArticleNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public List<ProductSupplierDto> Suppliers { get; set; } = [];
}

// Representerar en leverantör inuti en produkt för att visa vem som säljer den och till vilket pris
public class ProductSupplierDto
{
    public int SupplierId { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public decimal PricePerKg { get; set; }
}
