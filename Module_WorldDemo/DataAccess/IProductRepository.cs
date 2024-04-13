using Module_WorldDemo.Entities;

namespace Module_WorldDemo.DataAccess;

public interface IProductRepository
{
    Task<Product?> GetProductById(int id);
    Task<List<Product>> GetProducts();
}
