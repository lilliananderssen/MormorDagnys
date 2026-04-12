using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MormorDagnys.Data;
using MormorDagnys.DTOs.Products;

namespace MormorDagnys.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController(BakeryContext context) : ControllerBase
{
    // Hämtar alla produkter med tillhörande leverantörer för att ge en översikt av vad bageriet kan beställa
    [HttpGet]
    public async Task<ActionResult<List<GetProductDto>>> GetAll()
    {
        var products = await context.Products
            .Include(r => r.SupplierProducts)
                .ThenInclude(sp => sp.Supplier)
            .Select(r => new GetProductDto
            {
                ProductId = r.ProductId,
                ArticleNumber = r.ArticleNumber,
                Name = r.Name,
                Suppliers = r.SupplierProducts.Select(sp => new ProductSupplierDto
                {
                    SupplierId = sp.SupplierId,
                    SupplierName = sp.Supplier.Name,
                    PricePerKg = sp.PricePerKg
                }).ToList()
            })
            .ToListAsync();

        return Ok(products);
    }

    // Söker efter produkter på namn för att snabbt hitta en specifik ingrediens utan att bläddra igenom hela listan
    [HttpGet("search")]
    public async Task<ActionResult<List<GetProductDto>>> Search([FromQuery] string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest("Sökterm saknas.");

        var products = await context.Products
            .Include(r => r.SupplierProducts)
                .ThenInclude(sp => sp.Supplier)
            .Where(r => r.Name.ToLower().Contains(name.ToLower()))
            .Select(r => new GetProductDto
            {
                ProductId = r.ProductId,
                ArticleNumber = r.ArticleNumber,
                Name = r.Name,
                Suppliers = r.SupplierProducts.Select(sp => new ProductSupplierDto
                {
                    SupplierId = sp.SupplierId,
                    SupplierName = sp.Supplier.Name,
                    PricePerKg = sp.PricePerKg
                }).ToList()
            })
            .ToListAsync();

        return Ok(products);
    }
}
