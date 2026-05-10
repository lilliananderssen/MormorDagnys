using api.Extensions;
using api.DTOs.Orders;
using core.Entities;
using core.Entities.Orders;
using core.Interfaces;
using core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

public class OrdersController(IUnitOfWork uow) : ApiBaseController
{
    [HttpGet]
    public async Task<ActionResult> ListAllOrders(
        [FromQuery] string? orderNumber,
        [FromQuery] DateTime? orderDate)
    {
        var spec = new OrderSpecification(orderNumber, orderDate);
        var orders = await uow.Repository<Order>().ListAsync(spec);

        var result = orders.Select(o => new ListOrderDto
        {
            Id = o.Id,
            OrderNumber = o.OrderNumber,
            OrderDate = o.OrderDate,
            CustomerId = o.CustomerId,
            StoreName = o.Customer.StoreName,
            TotalAmount = o.OrderItems.Sum(i => i.Price * i.Quantity)
        }).ToList();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetOrderById(int id)
    {
        var spec = new OrderSpecification(id);
        var order = await uow.Repository<Order>().FindAsync(spec);
        if (order is null) return NotFound($"Beställning med id {id} hittades inte.");
        return Ok(order.ToDTO());
    }

    [HttpPost]
    public async Task<ActionResult> CreateOrder(PostOrderDto orderDto)
    {
        var customerExists = uow.Repository<Customer>().CheckIfExists(orderDto.CustomerId);
        if (!customerExists) return NotFound($"Kund med id {orderDto.CustomerId} hittades inte.");

        if (orderDto.Items is null || orderDto.Items.Count == 0)
            return BadRequest("Beställningen måste innehålla minst en produkt.");

        var items = new List<OrderItem>();

        foreach (var item in orderDto.Items)
        {
            var product = await uow.Repository<BakeryProduct>().FindByIdAsync(item.BakeryProductId);
            if (product is null) return NotFound($"Produkt med id {item.BakeryProductId} hittades inte.");

            items.Add(new OrderItem
            {
                BakeryProductId = product.Id,
                ProductName = product.Name,
                Price = product.PricePerUnit,
                Quantity = item.Quantity
            });
        }

        var order = new Order
        {
            CustomerId = orderDto.CustomerId,
            OrderDate = DateTime.Now,
            OrderItems = items
        };

        uow.Repository<Order>().Add(order);

        if (!await uow.Complete()) return StatusCode(500, "Något server fel inträffade");

        order.OrderNumber = $"ORD-{order.OrderDate:yyyyMMdd}-{order.Id:D4}";
        uow.Repository<Order>().Update(order);

        if (await uow.Complete()) return StatusCode(201, new { order.Id, order.OrderNumber });

        return StatusCode(500, "Något server fel inträffade");
    }
}
