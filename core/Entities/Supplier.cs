namespace core.Entities;

public class Supplier : BaseEntity
{
    public required string Name { get; set; }
    public required string Address { get; set; }
    public required string ContactPerson { get; set; }
    public required string Phone { get; set; }
    public required string Email { get; set; }
    public ICollection<SupplierProduct> SupplierProducts { get; set; } = [];
}
