namespace MormorDagnys.Entities;

// Representerar en leverantör för att lagra kontaktuppgifter och vilka produkter de erbjuder
public class Supplier
{
    public int SupplierId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string ContactPerson { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public ICollection<SupplierProduct> SupplierProducts { get; set; } = [];
}
