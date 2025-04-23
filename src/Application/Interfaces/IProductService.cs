using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(string id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(string id);
    }
}
