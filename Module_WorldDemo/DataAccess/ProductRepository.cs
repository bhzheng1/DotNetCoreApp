using Microsoft.EntityFrameworkCore;
using Module_WorldDemo.Entities;

namespace Module_WorldDemo.DataAccess;
public class ProductRepository : IProductRepository
{
    private readonly WorldDataBaseContext _context;
    public ProductRepository(WorldDataBaseContext context)
    {
        _context = context;
    }
    public async Task<Product?> GetProductById(int id) => await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

    public async Task<List<Product>> GetProducts()
    {
        return await _context.Products.ToListAsync();
    }
}
