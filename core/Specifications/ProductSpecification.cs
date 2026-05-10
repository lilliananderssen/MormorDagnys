using core.Entities;

namespace core.Specifications;

public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(ProductSpecificationParams args) : base(c =>
        (string.IsNullOrEmpty(args.Search) || c.Name.ToLower().Contains(args.Search.ToLower())) &&
        (string.IsNullOrWhiteSpace(args.ArticleNumber) || c.ArticleNumber == args.ArticleNumber))
    {
        ApplyPagination(args.PageSize, args.PageSize * (args.PageNumber - 1));

        switch (args.Sort)
        {
            default:
                UseOrderByAscending(c => c.Name);
                break;
        }
    }

    public ProductSpecification(int id) : base(c => c.Id == id) { }
}
