using api.Helpers;
using core.Interfaces;
using infrastructure.Data;
using infrastructure.Repositories;
using infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var useMySQL = builder.Configuration.GetValue<bool>("UseMySQL");

builder.Services.AddDbContext<BakeryContext>(options =>
{
    if (useMySQL)
    {
        var mysqlConn = builder.Configuration.GetConnectionString("MySqlConnection")!;
        options.UseMySql(mysqlConn, ServerVersion.AutoDetect(mysqlConn));
    }
    else
    {
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
});

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ISupplierProductService, SupplierProductService>();

builder.Services.AddAutoMapper(options =>
{
    options.AddProfile(new MappingProfiles());
});

var app = builder.Build();

app.MapControllers();

try
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<BakeryContext>();
    context.Database.EnsureCreated();
    await SeedDatabase.SeedSuppliers(context);
    await SeedDatabase.SeedProducts(context);
    await SeedDatabase.SeedCustomers(context);
    await SeedDatabase.SeedBakeryProducts(context);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

app.Run();
