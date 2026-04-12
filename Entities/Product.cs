namespace MormorDagnys.Entities;

// Representerar en produkt för att lagra information om ingredienser som bageriet köper in
public class Product
{
    public int ProductId { get; set; }
    public string ArticleNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public ICollection<SupplierProduct> SupplierProducts { get; set; } = [];
}
