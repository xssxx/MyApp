using Microsoft.EntityFrameworkCore;
using Infrastructure.DbContext;
using Infrastructure.Repositories;
using Application.Interfaces;
using Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

// การตั้งค่า SQLite connection string
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db")
);

// การลงทะเบียน Repository
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.MapGet("/", () => "Hello World!");

app.MapGet("/products", async (IProductRepository repository) =>
{
    var products = await repository.GetAllProductsAsync();
    return Results.Ok(products);
});

app.MapPost("/products", async (IProductRepository repository, Product product) =>
{
    await repository.AddProductAsync(product);
    return Results.Created($"/products/{product.Id}", product);
});

app.Run();
