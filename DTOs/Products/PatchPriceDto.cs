namespace MormorDagnys.DTOs.Products;

// Tar emot ett nytt pris från klienten för att uppdatera vad en leverantör tar för en specifik produkt
public class PatchPriceDto
{
    public decimal PricePerKg { get; set; }
}
