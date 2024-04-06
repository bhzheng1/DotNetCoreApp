using ClassLibrary_DataAccess.Entities;

namespace ClassLibrary_DataAccess.DataAccess;

public interface IProductRepository
{
    Task<Product?> GetProductById(int id);
    Task<List<Product>> GetProducts();
}
