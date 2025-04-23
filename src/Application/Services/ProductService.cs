using Application.Interfaces;
using Domain.Entities;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public Task<List<Product>> GetAllAsync() => _repository.GetAllProductsAsync();

    public Task<Product?> GetByIdAsync(Guid id) => _repository.GetProductByIdAsync(id)!;

    public Task AddAsync(Product product) => _repository.AddProductAsync(product);

    public Task UpdateAsync(Product product) => _repository.UpdateProductAsync(product);

    public Task DeleteAsync(Guid id) => _repository.DeleteProductAsync(id);
}
