using api.DTOs.BakeryProducts;
using AutoMapper;
using core.Entities;
using core.Interfaces;
using core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

public class BakeryProductsController(IUnitOfWork uow, IMapper mapper) : ApiBaseController
{
    [HttpGet]
    public async Task<ActionResult> ListAllBakeryProducts([FromQuery] BakeryProductSpecificationParams args)
    {
        var spec = new BakeryProductSpecification(args);
        var result = await uow.Repository<BakeryProduct>().ListAsync(spec);
        var products = mapper.Map<IReadOnlyList<GetBakeryProductDto>>(result);
        return await CreatePagedResult(uow.Repository<BakeryProduct>(), spec, args.PageNumber, args.PageSize, products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> FindBakeryProduct(int id)
    {
        var spec = new BakeryProductSpecification(id);
        var result = await uow.Repository<BakeryProduct>().FindAsync(spec);
        if (result is null) return NotFound($"Produkt med id {id} hittades inte.");
        var product = mapper.Map<GetBakeryProductDto>(result);
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult> AddBakeryProduct(PostBakeryProductDto model)
    {
        var product = mapper.Map<BakeryProduct>(model);
        uow.Repository<BakeryProduct>().Add(product);

        if (await uow.Complete()) return StatusCode(201);

        return StatusCode(500, "Något server fel inträffade");
    }

    [HttpPut("{id}/price")]
    public async Task<ActionResult> UpdatePrice(int id, PatchPriceDto model)
    {
        var product = await uow.Repository<BakeryProduct>().FindByIdAsync(id);
        if (product is null) return NotFound($"Produkt med id {id} hittades inte.");

        product.PricePerUnit = model.PricePerUnit;
        uow.Repository<BakeryProduct>().Update(product);

        if (await uow.Complete()) return NoContent();

        return BadRequest("Kunde inte uppdatera priset");
    }
}
