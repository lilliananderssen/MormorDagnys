namespace MormorDagnys.DTOs.Suppliers;

// Returnerar leverantörsdata till klienten för att visa leverantörens uppgifter och produkter
public class GetSupplierDto
{
    public int SupplierId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ContactPerson { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<SupplierProductDto> Products { get; set; } = [];
}

// Representerar en produkt inuti en leverantör för att visa vilka produkter leverantören erbjuder och till vilket pris
public class SupplierProductDto
{
    public int ProductId { get; set; }
    public string ArticleNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal PricePerKg { get; set; }
}
