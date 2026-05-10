namespace core.Specifications;

public class BaseSpecificationParams
{
    private const int MAX_PAGE_SIZE = 50;
    private int _pageSize = 30;

    public int PageNumber { get; set; } = 1;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MAX_PAGE_SIZE ? MAX_PAGE_SIZE : value;
    }

    public string? Sort { get; set; }
    public string? Search { get; set; }
}
