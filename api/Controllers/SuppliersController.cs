using api.DTOs.Suppliers;
using AutoMapper;
using core.Entities;
using core.Interfaces;
using core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

public class SuppliersController(IUnitOfWork uow, IMapper mapper, ISupplierProductService supplierProductService) : ApiBaseController
{
    [HttpGet]
    public async Task<ActionResult> ListAllSuppliers([FromQuery] SupplierSpecificationParams args)
    {
        var spec = new SupplierSpecification(args);
        var result = await uow.Repository<Supplier>().ListAsync(spec);
        var suppliers = mapper.Map<IReadOnlyList<GetSupplierDto>>(result);
        return await CreatePagedResult(uow.Repository<Supplier>(), spec, args.PageNumber, args.PageSize, suppliers);
    }

    [HttpGet("{id}/products")]
    public async Task<ActionResult> GetSupplierProducts(int id)
    {
        var spec = new SupplierSpecification(id);
        var supplier = await uow.Repository<Supplier>().FindAsync(spec);

        if (supplier is null) return NotFound($"Leverantör med id {id} hittades inte.");

        var result = new
        {
            supplier.Id,
            supplier.Name,
            supplier.ContactPerson,
            supplier.Email,
            Products = supplier.SupplierProducts.Select(sp => new
            {
                sp.ProductId,
                sp.Product.ArticleNumber,
                sp.Product.Name,
                sp.PricePerKg
            })
        };

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> AddSupplier(PostSupplierDto model)
    {
        var supplier = mapper.Map<Supplier>(model);
        uow.Repository<Supplier>().Add(supplier);

        if (await uow.Complete()) return StatusCode(201);

        return StatusCode(500, "Något server fel inträffade");
    }

    [HttpPost("{id}/products")]
    public async Task<ActionResult> AddProductToSupplier(int id, [FromBody] AddSupplierProductDto model)
    {
        var supplierExists = uow.Repository<Supplier>().CheckIfExists(id);
        if (!supplierExists) return NotFound($"Leverantör med id {id} hittades inte.");

        var productExists = uow.Repository<Product>().CheckIfExists(model.ProductId);
        if (!productExists) return NotFound($"Produkt med id {model.ProductId} hittades inte.");

        var alreadyExists = await supplierProductService.AnyAsync(id, model.ProductId);
        if (alreadyExists) return Conflict("Leverantören säljer redan denna produkt.");

        await supplierProductService.AddAsync(id, model.ProductId, model.PricePerKg);

        return StatusCode(201);
    }

    [HttpPut("{id}/products/{productId}/price")]
    public async Task<ActionResult> UpdateSupplierProductPrice(int id, int productId, [FromBody] PatchSupplierPriceDto model)
    {
        var entry = await supplierProductService.FindAsync(id, productId);

        if (entry is null) return NotFound($"Ingen koppling hittades mellan leverantör {id} och produkt {productId}.");

        entry.PricePerKg = model.PricePerKg;
        await supplierProductService.SaveChangesAsync();

        return NoContent();
    }
}

public class AddSupplierProductDto
{
    public int ProductId { get; set; }
    public decimal PricePerKg { get; set; }
}

public class PatchSupplierPriceDto
{
    public decimal PricePerKg { get; set; }
}
