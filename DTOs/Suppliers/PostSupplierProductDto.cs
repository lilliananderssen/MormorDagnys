namespace MormorDagnys.DTOs.Suppliers;

// Tar emot produkt och pris från klienten för att lägga till en ny produkt hos en leverantör
public class PostSupplierProductDto
{
    public int ProductId { get; set; }
    public decimal PricePerKg { get; set; }
}
