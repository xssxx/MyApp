using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Product
{
    // Auto generate UUID
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
