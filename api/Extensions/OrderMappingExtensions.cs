using api.DTOs.Customers;
using core.Entities.Orders;

namespace api.Extensions;

public static class OrderMappingExtensions
{
    public static OrderDto ToDTO(this Order order)
    {
        return new OrderDto
        {
            Id = order.Id,
            OrderNumber = order.OrderNumber,
            OrderDate = order.OrderDate,
            Customer = new OrderCustomerDto
            {
                Id = order.Customer.Id,
                StoreName = order.Customer.StoreName,
                Phone = order.Customer.Phone,
                Email = order.Customer.Email,
                ContactPerson = order.Customer.ContactPerson
            },
            OrderItems = [.. order.OrderItems.Select(i => i.ToDTO())],
            TotalAmount = order.OrderItems.Sum(i => i.Price * i.Quantity)
        };
    }

    public static OrderItemDto ToDTO(this OrderItem item)
    {
        return new OrderItemDto
        {
            BakeryProductId = item.BakeryProductId,
            ProductName = item.ProductName,
            Quantity = item.Quantity,
            Price = item.Price,
            SubTotal = item.Price * item.Quantity
        };
    }
}

public class OrderDto
{
    public int Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    public required OrderCustomerDto Customer { get; set; }
    public required List<OrderItemDto> OrderItems { get; set; }
    public decimal TotalAmount { get; set; }
}

public class OrderCustomerDto
{
    public int Id { get; set; }
    public required string StoreName { get; set; }
    public required string Phone { get; set; }
    public required string Email { get; set; }
    public required string ContactPerson { get; set; }
}

public class OrderItemDto
{
    public int BakeryProductId { get; set; }
    public required string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal SubTotal { get; set; }
}

public class ListOrderDto
{
    public int Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    public int CustomerId { get; set; }
    public string StoreName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
}
