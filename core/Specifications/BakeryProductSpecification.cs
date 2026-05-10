using core.Entities;

namespace core.Specifications;

public class BakeryProductSpecification : BaseSpecification<BakeryProduct>
{
    public BakeryProductSpecification(BakeryProductSpecificationParams args) : base(c =>
        string.IsNullOrEmpty(args.Search) || c.Name.ToLower().Contains(args.Search.ToLower()))
    {
        ApplyPagination(args.PageSize, args.PageSize * (args.PageNumber - 1));

        switch (args.Sort)
        {
            case "priceAsc":
                UseOrderByAscending(c => c.PricePerUnit);
                break;
            case "priceDesc":
                UseOrderByDescending(c => c.PricePerUnit);
                break;
            default:
                UseOrderByAscending(c => c.Name);
                break;
        }
    }

    public BakeryProductSpecification(int id) : base(c => c.Id == id) { }
}
