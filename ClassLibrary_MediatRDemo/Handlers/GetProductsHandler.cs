using MediatR;
using ClassLibrary_MediatRDemo.Queries;
namespace ClassLibrary_MediatRDemo.Handlers;
public class GetProductsHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
{
    private readonly FakeDataStore _fakeDataStore;
    public GetProductsHandler(FakeDataStore fakeDataStore)
    {
        _fakeDataStore = fakeDataStore;
    }
    public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return await _fakeDataStore.GetProducts();
    }
}
