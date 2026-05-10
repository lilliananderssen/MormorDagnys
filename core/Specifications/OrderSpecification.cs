using core.Entities.Orders;

namespace core.Specifications;

public class OrderSpecification : BaseSpecification<Order>
{
    public OrderSpecification(string? orderNumber, DateTime? orderDate) : base(c =>
        (string.IsNullOrWhiteSpace(orderNumber) || c.OrderNumber.Contains(orderNumber)) &&
        (!orderDate.HasValue || c.OrderDate.Date == orderDate.Value.Date))
    {
        AddInclude(c => c.Customer);
        AddInclude(c => c.OrderItems);
        UseOrderByDescending(c => c.OrderDate);
    }

    public OrderSpecification(int id) : base(c => c.Id == id)
    {
        AddInclude(c => c.Customer);
        AddInclude("OrderItems");
    }

    public OrderSpecification(int customerId, bool byCustomer) : base(c => c.CustomerId == customerId)
    {
        AddInclude(c => c.OrderItems);
        UseOrderByDescending(c => c.OrderDate);
    }
}
