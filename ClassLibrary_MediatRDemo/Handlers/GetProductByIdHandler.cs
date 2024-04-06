using MediatR;
using ClassLibrary_MediatRDemo.Queries;
namespace ClassLibrary_MediatRDemo.Handlers;

public class GetProductByIdHandler : IRequestHandler<GetProductById, Product>
{
    private readonly FakeDataStore _fakeDataStore;
    public GetProductByIdHandler(FakeDataStore fakeDataStore)
    {
        _fakeDataStore = fakeDataStore;
    }

    public async Task<Product> Handle(GetProductById request, CancellationToken cancellationToken)
    {
        return await _fakeDataStore.GetProductById(request.Id);
    }
}
