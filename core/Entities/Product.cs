namespace core.Entities;

public class Product : BaseEntity
{
    public required string ArticleNumber { get; set; }
    public required string Name { get; set; }
    public ICollection<SupplierProduct> SupplierProducts { get; set; } = [];
}
