using api.DTOs.Customers;
using api.Extensions;
using AutoMapper;
using core.Entities;
using core.Entities.Orders;
using core.Interfaces;
using core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

public class CustomersController(IUnitOfWork uow, IMapper mapper) : ApiBaseController
{
    [HttpGet]
    public async Task<ActionResult> ListAllCustomers([FromQuery] CustomerSpecificationParams args)
    {
        var spec = new CustomerSpecification(args);
        var result = await uow.Repository<Customer>().ListAsync(spec);
        var customers = mapper.Map<IReadOnlyList<GetCustomerDto>>(result);
        return await CreatePagedResult(uow.Repository<Customer>(), spec, args.PageNumber, args.PageSize, customers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetCustomerById(int id)
    {
        var spec = new CustomerSpecification(id);
        var customer = await uow.Repository<Customer>().FindAsync(spec);

        if (customer is null) return NotFound($"Kund med id {id} hittades inte.");

        return Ok(customer.ToDetailDTO());
    }

    [HttpPost]
    public async Task<ActionResult> AddCustomer(PostCustomerDto model)
    {
        var customer = mapper.Map<Customer>(model);
        uow.Repository<Customer>().Add(customer);

        if (await uow.Complete()) return StatusCode(201);

        return StatusCode(500, "Något server fel inträffade");
    }

    [HttpPut("{id}/contact-person")]
    public async Task<ActionResult> UpdateContactPerson(int id, PatchContactPersonDto model)
    {
        var customer = await uow.Repository<Customer>().FindByIdAsync(id);

        if (customer is null) return NotFound($"Kund med id {id} hittades inte.");

        customer.ContactPerson = model.ContactPerson;
        uow.Repository<Customer>().Update(customer);

        if (await uow.Complete()) return NoContent();

        return BadRequest("Kunde inte uppdatera kontaktperson");
    }

    [HttpGet("{id}/products")]
    public async Task<ActionResult> GetPurchasedProducts(int id)
    {
        if (!uow.Repository<Customer>().CheckIfExists(id))
            return NotFound($"Kund med id {id} hittades inte.");

        var spec = new OrderSpecification(id, byCustomer: true);
        var orders = await uow.Repository<Order>().ListAsync(spec);

        var result = orders
            .SelectMany(o => o.OrderItems)
            .GroupBy(i => new { i.BakeryProductId, i.ProductName })
            .Select(g => new
            {
                BakeryProductId = g.Key.BakeryProductId,
                ProductName = g.Key.ProductName,
                TotalQuantity = g.Sum(i => i.Quantity),
                TotalSpent = g.Sum(i => i.Price * i.Quantity)
            })
            .ToList();

        return Ok(result);
    }
}
