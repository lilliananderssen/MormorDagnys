using core.Entities;
using core.Interfaces;
using core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

public class ProductsController(IUnitOfWork uow) : ApiBaseController
{
    [HttpGet]
    public async Task<ActionResult> ListAllProducts([FromQuery] ProductSpecificationParams args)
    {
        var spec = new ProductSpecification(args);
        var result = await uow.Repository<Product>().ListAsync(spec);
        return await CreatePagedResult(uow.Repository<Product>(), spec, args.PageNumber, args.PageSize, result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> FindProduct(int id)
    {
        var spec = new ProductSpecification(id);
        var result = await uow.Repository<Product>().FindAsync(spec);
        if (result is null) return NotFound($"Råvara med id {id} hittades inte.");
        return Ok(result);
    }
}
