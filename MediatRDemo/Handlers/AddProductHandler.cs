using MediatR;
using MediatRDemo.Commands;

namespace MediatRDemo.Handlers;

public class AddProductHandler : IRequestHandler<AddProductCommand, Product>
{
    private readonly FakeDataStore _fakeDataStore;
    public AddProductHandler(FakeDataStore fakeDataStore)
    {
        _fakeDataStore = fakeDataStore;
    }
    public async Task<Product> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product { Id = request.Id, Name = request.Name };
        await _fakeDataStore.AddProduct(product);
        return product;
    }
}
