using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MormorDagnys.Data;
using MormorDagnys.DTOs.Products;
using MormorDagnys.DTOs.Suppliers;
using MormorDagnys.Entities;

namespace MormorDagnys.Controllers;

[Route("api/suppliers")]
[ApiController]
public class SuppliersController(BakeryContext context) : ControllerBase
{
    // Hämtar en leverantör med alla dess produkter för att se vad en specifik leverantör erbjuder och till vilka priser
    [HttpGet("{id}/products")]
    public async Task<ActionResult<GetSupplierDto>> GetSupplierProducts(int id)
    {
        var supplier = await context.Suppliers
            .Include(s => s.SupplierProducts)
                .ThenInclude(sp => sp.Product)
            .Where(s => s.SupplierId == id)
            .Select(s => new GetSupplierDto
            {
                SupplierId = s.SupplierId,
                Name = s.Name,
                ContactPerson = s.ContactPerson,
                Email = s.Email,
                Products = s.SupplierProducts.Select(sp => new SupplierProductDto
                {
                    ProductId = sp.ProductId,
                    ArticleNumber = sp.Product.ArticleNumber,
                    Name = sp.Product.Name,
                    PricePerKg = sp.PricePerKg
                }).ToList()
            })
            .FirstOrDefaultAsync();

        if (supplier is null)
            return NotFound($"Leverantör med id {id} hittades inte.");

        return Ok(supplier);
    }

    // Lägger till en produkt hos en leverantör för att registrera att leverantören säljer den till ett visst pris
    [HttpPost("{id}/products")]
    public async Task<ActionResult> AddProduct(int id, PostSupplierProductDto model)
    {
        var supplierExists = await context.Suppliers.AnyAsync(s => s.SupplierId == id);
        if (!supplierExists)
            return NotFound($"Leverantör med id {id} hittades inte.");

        var productExists = await context.Products.AnyAsync(r => r.ProductId == model.ProductId);
        if (!productExists)
            return NotFound($"Produkt med id {model.ProductId} hittades inte.");

        var alreadyExists = await context.SupplierProducts
            .AnyAsync(sp => sp.SupplierId == id && sp.ProductId == model.ProductId);
        if (alreadyExists)
            return Conflict("Leverantören säljer redan denna produkt.");

        var entry = new SupplierProduct
        {
            SupplierId = id,
            ProductId = model.ProductId,
            PricePerKg = model.PricePerKg
        };

        context.SupplierProducts.Add(entry);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSupplierProducts), new { id }, null);
    }

    // Uppdaterar priset på en produkt hos en leverantör för att hålla prislistan aktuell
    [HttpPatch("{id}/products/{productId}/price")]
    public async Task<ActionResult> UpdatePrice(int id, int productId, PatchPriceDto model)
    {
        var entry = await context.SupplierProducts
            .FirstOrDefaultAsync(sp => sp.SupplierId == id && sp.ProductId == productId);

        if (entry is null)
            return NotFound($"Ingen koppling hittades mellan leverantör {id} och produkt {productId}.");

        entry.PricePerKg = model.PricePerKg;
        await context.SaveChangesAsync();

        return NoContent();
    }
}
