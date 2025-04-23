using Microsoft.EntityFrameworkCore;
using Infrastructure.DbContext;
using Infrastructure.Repositories;
using Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// การตั้งค่า SQLite connection string
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db")
);

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddControllers();

var app = builder.Build();

app.UsePathBase("/api");

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.MapGet("/", () => "Hello World!");

app.MapControllers();

app.Run();
