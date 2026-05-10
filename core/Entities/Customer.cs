using core.Entities.Orders;

namespace core.Entities;

public class Customer : BaseEntity
{
    public required string StoreName { get; set; }
    public required string Phone { get; set; }
    public required string Email { get; set; }
    public required string ContactPerson { get; set; }
    public CustomerAddress DeliveryAddress { get; set; } = null!;
    public CustomerAddress InvoiceAddress { get; set; } = null!;
    public ICollection<Order> Orders { get; set; } = [];
}
