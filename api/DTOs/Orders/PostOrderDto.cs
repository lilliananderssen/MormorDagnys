using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Orders;

public class PostOrderDto
{
    [Required]
    public int CustomerId { get; set; }
    [Required]
    public List<PostOrderItemDto> Items { get; set; } = [];
}

public class PostOrderItemDto
{
    public int BakeryProductId { get; set; }
    public int Quantity { get; set; }
}
