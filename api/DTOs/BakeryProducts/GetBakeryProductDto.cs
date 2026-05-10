namespace api.DTOs.BakeryProducts;

public class GetBakeryProductDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal PricePerUnit { get; set; }
    public decimal WeightGrams { get; set; }
    public int UnitsInPackaging { get; set; }
    public DateTime BestBefore { get; set; }
    public DateTime ManufacturingDate { get; set; }
}
