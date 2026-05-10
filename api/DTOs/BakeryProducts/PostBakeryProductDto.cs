using System.ComponentModel.DataAnnotations;

namespace api.DTOs.BakeryProducts;

public class PostBakeryProductDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Range(0.0, double.MaxValue, ErrorMessage = "Priset måste vara större än 0")]
    public decimal PricePerUnit { get; set; }
    [Range(0.0, double.MaxValue, ErrorMessage = "Vikten måste vara större än 0")]
    public decimal WeightGrams { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Antal i förpackning måste vara minst 1")]
    public int UnitsInPackaging { get; set; }
    public DateTime BestBefore { get; set; }
    public DateTime ManufacturingDate { get; set; }
}
