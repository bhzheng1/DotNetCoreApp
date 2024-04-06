using ClassLibrary_DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary_DataAccess.DataAccess;
public class ProductRepository : IProductRepository
{
    private readonly ClDataBaseContext _context;
    public ProductRepository(ClDataBaseContext context)
    {
        _context = context;
    }
    public async Task<Product?> GetProductById(int id) => await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

    public async Task<List<Product>> GetProducts()
    {
        return await _context.Products.ToListAsync();
    }
}
