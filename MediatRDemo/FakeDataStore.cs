namespace MediatRDemo;

public class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }
}

public class FakeDataStore
{
    private static List<Product> _products = [];

    public FakeDataStore()
    {
        _products =
        [
            new Product { Id = 1, Name = "Product 1" },
            new Product { Id = 2, Name = "Product 2" },
            new Product { Id = 3, Name = "Product 3" },
            new Product { Id = 4, Name = "Product 4" },
        ];
    }

    public async Task AddProduct(Product product)
    {
        _products.Add(product);
        await Task.CompletedTask;
    }

    public async Task<IEnumerable<Product>> GetProducts() => await Task.FromResult(_products);
    public async Task<Product> GetProductById(int id) => await Task.FromResult(_products.Single(x => x.Id == id));

    public async Task EventOccurred(Product product, string evt)
    {
        _products.Single(p => p.Id == product.Id).Name = $"{product.Name} evt: {evt}";
        await Task.CompletedTask;
    }
}