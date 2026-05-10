using core.Entities;
using core.Entities.Orders;

namespace core.Specifications;

public class CustomerSpecification : BaseSpecification<Customer>
{
    public CustomerSpecification(CustomerSpecificationParams args) : base(c =>
        string.IsNullOrWhiteSpace(args.StoreName) || c.StoreName.ToLower().Contains(args.StoreName.ToLower()))
    {
        ApplyPagination(args.PageSize, args.PageSize * (args.PageNumber - 1));

        switch (args.Sort)
        {
            default:
                UseOrderByAscending(c => c.StoreName);
                break;
        }
    }

    public CustomerSpecification(int id) : base(c => c.Id == id)
    {
        AddInclude(c => c.Orders);
        AddInclude("Orders.OrderItems");
    }
}
