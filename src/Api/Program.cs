using Application.Interfaces;
using Infrastructure.DbContext;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ตั้งค่า SQLite connection string
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=app.db"));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

// app.UseHttpsRedirection();
app.UseMiddleware<LoggingMiddleware>();
app.UsePathBase("/api");
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/api/swagger/v1/swagger.json", "My API V1");
    options.RoutePrefix = "swagger";
});

// Default route
app.MapGet("/", () => "Hello World!");

app.MapControllers();

app.Run();
